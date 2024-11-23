using Maksov.AwsTutorial.ToDoList.BLL.Models;
using MediatR;

namespace Maksov.AwsTutorial.ToDoList.BLL;

public sealed record GetAllToDosQuery : IRequest<ICollection<ToDoItemDto>>;