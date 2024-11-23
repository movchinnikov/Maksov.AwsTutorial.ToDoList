using MediatR;

namespace Maksov.AwsTutorial.ToDoList.BLL;

public sealed record DeleteToDoCommand(int Id): IRequest;