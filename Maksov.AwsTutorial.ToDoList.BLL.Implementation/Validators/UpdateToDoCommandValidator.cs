using FluentValidation;

namespace Maksov.AwsTutorial.ToDoList.BLL.Implementation.Validators;

public sealed class UpdateToDoCommandValidator : AbstractValidator<UpdateToDoCommand>
{
    public UpdateToDoCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be a positive number.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(150)
            .WithMessage("Title must not exceed 150 characters.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid status. Allowed values are: ToDo, InProgress, Done, Cancelled.");
    }
}