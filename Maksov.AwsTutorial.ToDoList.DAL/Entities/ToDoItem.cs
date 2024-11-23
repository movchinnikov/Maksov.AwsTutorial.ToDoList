namespace Maksov.AwsTutorial.ToDoList.DAL.Entities;

public sealed record ToDoItem(string Title, ItemStatus Status)
{
    public int Id { get; set; }
};