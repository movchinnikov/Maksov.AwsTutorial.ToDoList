using Maksov.AwsTutorial.ToDoList.DAL.Entities;

namespace Maksov.AwsTutorial.ToDoList.DAL.Implementation;

public sealed class ToDoRepository : IToDoRepository
{
    private int _sequenceId;
    private readonly List<ToDoItem> _items = new();

    public Task<ICollection<ToDoItem>> GetAllAsync() => 
        Task.FromResult<ICollection<ToDoItem>>(_items);

    public Task<ToDoItem?> GetByIdAsync(int id) =>
        Task.FromResult(_items.FirstOrDefault(x => x.Id == id));

    public Task AddAsync(ToDoItem item)
    {
        item.Id = ++_sequenceId;
        _items.Add(item);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(ToDoItem item)
    {
        var index = _items.FindIndex(x => x.Id == item.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"ToDo item with ID {item.Id} not found.");
        }

        _items[index] = item;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var index = _items.FindIndex(x => x.Id == id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"ToDo item with ID {id} not found.");
        }

        _items.RemoveAll(x => x.Id == id);
        return Task.CompletedTask;
    }
}