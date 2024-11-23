using System.Text.Json.Serialization;

namespace Maksov.AwsTutorial.ToDoList.Api.Models;

public sealed record ToDoItemCreateRequest(
    [property: JsonPropertyName("title")] string Title);