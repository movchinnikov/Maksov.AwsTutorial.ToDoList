using System.Data;
using System.Text.Json;
using Carter;
using FluentValidation;
using Maksov.AwsTutorial.ToDoList.Api.Mappings;
using Maksov.AwsTutorial.ToDoList.Api.Middlewares;
using Maksov.AwsTutorial.ToDoList.BLL;
using Maksov.AwsTutorial.ToDoList.BLL.Implementation;
using Maksov.AwsTutorial.ToDoList.BLL.Implementation.Validators;
using Maksov.AwsTutorial.ToDoList.DAL;
using Maksov.AwsTutorial.ToDoList.DAL.Config;
using Maksov.AwsTutorial.ToDoList.DAL.Implementation;
using Maksov.AwsTutorial.ToDoList.DAL.Migrations;
using Mapster;
using MapsterMapper;
using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.Configure<DatabaseConfig>(config =>
{
    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
    if (!string.IsNullOrWhiteSpace(connectionString))
    {
        config = new DatabaseConfig { ConnectionString = connectionString};
    }
    builder.Configuration.GetSection("Database").Bind(config);
});

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var config = sp.GetRequiredService<IOptions<DatabaseConfig>>().Value;
    return new NpgsqlConnection(config.ConnectionString);
});
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
typeAdapterConfig.Scan(
    typeof(ToDoItemMappingConfig).Assembly,
    typeof(Maksov.AwsTutorial.ToDoList.BLL.Implementation.Mappings.ToDoItemMappingConfig).Assembly);
var mapperConfig = new Mapper(typeAdapterConfig);
builder.Services.AddSingleton<IMapper>(mapperConfig);

// Add services
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(
        typeof(CreateToDoCommand).Assembly,
        typeof(CreateToDoCommandHandler).Assembly
    );
});
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddValidatorsFromAssemblyContaining<GetByIdToDoQueryValidator>();

/*builder.Services.Scan(scan => scan
    .FromAssembliesOf(typeof(IToDoRepository))
    .AddClasses()
    .AsImplementedInterfaces()
    .WithScopedLifetime());*/

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ToDoList API",
        Version = "v1",
        Description = "A simple ToDo List API using Minimal API, MediatR, Mapster and CQRS"
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseConfig>>();
    MigrationRunner.RunMigrations(config);
}

// Enable Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API v1");
    options.RoutePrefix = string.Empty;
});

app.UseRouting();
app.MapCarter();
app.Run();