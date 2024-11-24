using DbUp;
using Microsoft.Extensions.Options;
using Maksov.AwsTutorial.ToDoList.DAL.Config;

namespace Maksov.AwsTutorial.ToDoList.DAL.Migrations;

public static class MigrationRunner
{
    public static void RunMigrations(IOptions<DatabaseConfig> databaseConfig)
    {
        var connectionString = databaseConfig.Value.ConnectionString;

        var upgrader = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(typeof(MigrationRunner).Assembly)
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine($"Migration failed: {result.Error}");
            throw result.Error;
        }

        Console.WriteLine("Migration completed successfully.");
    }
}