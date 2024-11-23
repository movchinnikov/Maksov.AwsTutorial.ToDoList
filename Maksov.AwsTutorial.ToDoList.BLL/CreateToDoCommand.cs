using Maksov.AwsTutorial.ToDoList.BLL.Models;
using MediatR;

namespace Maksov.AwsTutorial.ToDoList.BLL;

public sealed record CreateToDoCommand(string Title) : IRequest<ToDoItemDto>;