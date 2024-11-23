using FluentValidation;
using Maksov.AwsTutorial.ToDoList.BLL.Models;
using Maksov.AwsTutorial.ToDoList.DAL;
using Mapster;
using MediatR;

namespace Maksov.AwsTutorial.ToDoList.BLL.Implementation;

public sealed class GetByIdToDoQueryHandler(IToDoRepository repository, IValidator<GetByIdToDoQuery> validator)
    : IRequestHandler<GetByIdToDoQuery, ToDoItemDto?>
{
    public async Task<ToDoItemDto?> Handle(GetByIdToDoQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var entity =  await repository.GetByIdAsync(request.Id);
        return entity?.Adapt<ToDoItemDto>();
    }
}