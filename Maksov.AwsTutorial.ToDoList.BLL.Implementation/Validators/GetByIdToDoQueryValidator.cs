using FluentValidation;

namespace Maksov.AwsTutorial.ToDoList.BLL.Implementation.Validators;

public sealed class GetByIdToDoQueryValidator : AbstractValidator<GetByIdToDoQuery>
{
    public GetByIdToDoQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .GreaterThan(0)
            .WithMessage("Id must be a positive number.");
    }
}