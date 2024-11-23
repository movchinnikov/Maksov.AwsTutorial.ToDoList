using Maksov.AwsTutorial.ToDoList.Api.Models;
using Maksov.AwsTutorial.ToDoList.BLL.Models;
using Mapster;

namespace Maksov.AwsTutorial.ToDoList.Api.Mappings;

public sealed class ToDoItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ToDoItemDto, ToDoItemResponse>();
    }
}