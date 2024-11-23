using Maksov.AwsTutorial.ToDoList.DAL.Entities;

namespace Maksov.AwsTutorial.ToDoList.DAL;

public interface IToDoRepository
{
    Task<ICollection<ToDoItem>> GetAllAsync();
    Task<ToDoItem?> GetByIdAsync(int id);
    Task AddAsync(ToDoItem item);
    Task UpdateAsync(ToDoItem item);
    Task DeleteAsync(int id);
}