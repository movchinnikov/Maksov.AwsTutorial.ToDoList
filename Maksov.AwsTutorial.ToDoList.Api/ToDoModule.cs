using Carter;
using Maksov.AwsTutorial.ToDoList.Api.Models;
using Maksov.AwsTutorial.ToDoList.BLL;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Maksov.AwsTutorial.ToDoList.Api;

public class ToDoModule : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        // Get all ToDo items
        app.MapGet("/todos", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllToDosQuery());
            var mappedResult = result.Adapt<ICollection<ToDoItemResponse>>();

            return mappedResult.Any() ? Results.Ok(mappedResult) : Results.NoContent();
        })
        .WithName("GetAllToDos")
        .WithTags("ToDo Operations")
        .Produces<ICollection<ToDoItemResponse>>()
        .Produces(StatusCodes.Status204NoContent);

        // Get a single ToDo item by ID
        app.MapGet("/todos/{id:int}", async (int id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetByIdToDoQuery(id));
            return result is not null ? Results.Ok(result.Adapt<ToDoItemResponse>()) : Results.NotFound();
        })
        .WithName("GetToDoById")
        .WithTags("ToDo Operations")
        .Produces<ToDoItemResponse>()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);

        // Create a new ToDo item
        app.MapPost("/todos", async ([FromBody] ToDoItemCreateRequest request, IMediator mediator) =>
        {
            var command = new CreateToDoCommand(request.Title);
            var result = await mediator.Send(command);
            return Results.Created($"/todos/{result.Id}", result.Adapt<ToDoItemResponse>());
        })
        .WithName("CreateToDo")
        .WithTags("ToDo Operations")
        .Accepts<ToDoItemCreateRequest>("application/json")
        .Produces<ToDoItemResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        // Update an existing ToDo item
        app.MapPut("/todos/{id:int}", async (int id, [FromBody] ToDoItemUpdateRequest model, IMediator mediator) =>
        {
            var command = new UpdateToDoCommand(id, model.Title, model.Status);
            await mediator.Send(command);
            return Results.Ok();
        })
        .WithName("UpdateToDo")
        .WithTags("ToDo Operations")
        .Accepts<ToDoItemUpdateRequest>("application/json")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        // Delete a ToDo item by ID
        app.MapDelete("/todos/{id:int}", async (int id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteToDoCommand(id));
            return Results.Ok();
        })
        .WithName("DeleteToDo")
        .WithTags("ToDo Operations")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status400BadRequest);
    }
}