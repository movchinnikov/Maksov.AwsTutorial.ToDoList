namespace Maksov.AwsTutorial.ToDoList.DAL.Entities;

public sealed record ToDoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public ItemStatus Status { get; set; }

    public ToDoItem() {}

    public ToDoItem(string title, ItemStatus status)
    {
        Title = title;
        Status = status;
    }
}