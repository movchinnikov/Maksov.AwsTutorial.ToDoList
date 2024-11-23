using FluentValidation;
using Maksov.AwsTutorial.ToDoList.BLL.Models;
using Maksov.AwsTutorial.ToDoList.DAL;
using Maksov.AwsTutorial.ToDoList.DAL.Entities;
using Mapster;
using MediatR;
using ItemStatus = Maksov.AwsTutorial.ToDoList.DAL.Entities.ItemStatus;

namespace Maksov.AwsTutorial.ToDoList.BLL.Implementation;

public sealed class CreateToDoCommandHandler(IToDoRepository repository, IValidator<CreateToDoCommand> validator)
    : IRequestHandler<CreateToDoCommand, ToDoItemDto>
{
    public async Task<ToDoItemDto> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var item = new ToDoItem(request.Title, ItemStatus.ToDo);
        await repository.AddAsync(item);
        return item.Adapt<ToDoItemDto>();
    }
}