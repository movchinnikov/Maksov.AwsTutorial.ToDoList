using FluentValidation;
using Maksov.AwsTutorial.ToDoList.BLL.Models;
using Maksov.AwsTutorial.ToDoList.DAL;
using Maksov.AwsTutorial.ToDoList.DAL.Entities;
using Mapster;
using MediatR;
using ItemStatus = Maksov.AwsTutorial.ToDoList.DAL.Entities.ItemStatus;

namespace Maksov.AwsTutorial.ToDoList.BLL.Implementation;

public sealed class UpdateToDoCommandHandler(IToDoRepository repository, IValidator<UpdateToDoCommand> validator)
    : IRequestHandler<UpdateToDoCommand>
{
    public async Task Handle(UpdateToDoCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var entity = command.Adapt<ToDoItem>();
        await repository.UpdateAsync(entity);
    }
}