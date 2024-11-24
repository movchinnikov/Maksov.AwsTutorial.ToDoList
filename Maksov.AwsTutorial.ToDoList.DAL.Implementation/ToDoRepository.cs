using System.Data;
using Dapper;
using Maksov.AwsTutorial.ToDoList.DAL.Entities;

namespace Maksov.AwsTutorial.ToDoList.DAL.Implementation;

public class ToDoRepository : IToDoRepository
{
    private readonly IDbConnection _dbConnection;

    public ToDoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<ToDoItem>> GetAllAsync()
    {
        const string query = "SELECT Id, Title, Status FROM ToDoItems;";
        return await _dbConnection.QueryAsync<ToDoItem>(query);
    }

    public async Task<ToDoItem?> GetByIdAsync(int id)
    {
        const string query = "SELECT Id, Title, Status FROM ToDoItems WHERE Id = @Id;";
        return await _dbConnection.QueryFirstOrDefaultAsync<ToDoItem>(query, new { Id = id });
    }

    public async Task AddAsync(ToDoItem item)
    {
        const string query = @"
            INSERT INTO ToDoItems (Title, Status)
            VALUES (@Title, @Status::INTEGER)
            RETURNING Id;";
        item.Id = await _dbConnection.ExecuteScalarAsync<int>(query, item);
    }

    public async Task UpdateAsync(ToDoItem item)
    {
        const string query = @"
            UPDATE ToDoItems
            SET Title = @Title, Status = @Status::INTEGER
            WHERE Id = @Id;";
        await _dbConnection.ExecuteAsync(query, item);
    }

    public async Task DeleteAsync(int id)
    {
        const string query = "DELETE FROM ToDoItems WHERE Id = @Id;";
        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }
}