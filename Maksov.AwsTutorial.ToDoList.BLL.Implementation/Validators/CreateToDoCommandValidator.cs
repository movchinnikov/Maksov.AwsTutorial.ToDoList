using FluentValidation;

namespace Maksov.AwsTutorial.ToDoList.BLL.Implementation.Validators;

public sealed class CreateToDoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    public CreateToDoCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(150)
            .WithMessage("Title must not exceed 150 characters.");
    }
}