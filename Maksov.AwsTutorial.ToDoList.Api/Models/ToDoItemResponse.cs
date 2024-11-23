using System.Text.Json.Serialization;
using Maksov.AwsTutorial.ToDoList.BLL.Models;

namespace Maksov.AwsTutorial.ToDoList.Api.Models;

public sealed record ToDoItemResponse(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("status")] ItemStatus Status);