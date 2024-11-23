namespace Maksov.AwsTutorial.ToDoList.BLL.Models;

public sealed record ToDoItemDto(int Id, string Title, ItemStatus Status);