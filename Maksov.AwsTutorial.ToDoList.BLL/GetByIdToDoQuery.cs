using Maksov.AwsTutorial.ToDoList.BLL.Models;
using MediatR;

namespace Maksov.AwsTutorial.ToDoList.BLL;

public sealed record GetByIdToDoQuery(int Id) : IRequest<ToDoItemDto?>;