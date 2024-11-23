using FluentValidation;
using Maksov.AwsTutorial.ToDoList.DAL;
using MediatR;

namespace Maksov.AwsTutorial.ToDoList.BLL.Implementation;

public class DeleteToDoCommandHandler(IToDoRepository repository, IValidator<DeleteToDoCommand> validator)
    : IRequestHandler<DeleteToDoCommand>
{
    public async Task Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await repository.DeleteAsync(request.Id);
    }
}