using Maksov.AwsTutorial.ToDoList.BLL.Models;
using MediatR;

namespace Maksov.AwsTutorial.ToDoList.BLL;

public sealed record UpdateToDoCommand(int Id, string Title, ItemStatus Status) : IRequest;