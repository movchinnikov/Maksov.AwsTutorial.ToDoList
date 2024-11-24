namespace Maksov.AwsTutorial.ToDoList.DAL.Config;

public sealed record DatabaseConfig
{
    public string? ConnectionString { get; set; }
};