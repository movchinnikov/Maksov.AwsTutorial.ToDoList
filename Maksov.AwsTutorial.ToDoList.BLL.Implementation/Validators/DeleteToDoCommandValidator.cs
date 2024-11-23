using FluentValidation;

namespace Maksov.AwsTutorial.ToDoList.BLL.Implementation.Validators;

public sealed class DeleteToDoCommandValidator : AbstractValidator<DeleteToDoCommand>
{
    public DeleteToDoCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be a positive number.");
    }
}