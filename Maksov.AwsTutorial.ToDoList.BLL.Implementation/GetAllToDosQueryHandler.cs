using Maksov.AwsTutorial.ToDoList.BLL.Models;
using Maksov.AwsTutorial.ToDoList.DAL;
using Mapster;
using MediatR;

namespace Maksov.AwsTutorial.ToDoList.BLL.Implementation;

public sealed class GetAllToDosQueryHandler(IToDoRepository repository)
    : IRequestHandler<GetAllToDosQuery, ICollection<ToDoItemDto>>
{
    public async Task<ICollection<ToDoItemDto>> Handle(GetAllToDosQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync();
        var mappedResult = entities.Adapt<ICollection<ToDoItemDto>>();
        return mappedResult;
    }
}