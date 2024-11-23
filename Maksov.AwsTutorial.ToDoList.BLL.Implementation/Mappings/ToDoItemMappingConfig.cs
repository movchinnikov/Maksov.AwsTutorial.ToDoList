using Maksov.AwsTutorial.ToDoList.BLL.Models;
using Maksov.AwsTutorial.ToDoList.DAL.Entities;
using Mapster;

namespace Maksov.AwsTutorial.ToDoList.BLL.Implementation.Mappings;

public sealed class ToDoItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ToDoItem, ToDoItemDto>();
        config.NewConfig<Models.ItemStatus, DAL.Entities.ItemStatus>();
        config.NewConfig<DAL.Entities.ItemStatus, Models.ItemStatus>();
        config.NewConfig<UpdateToDoCommand, ToDoItem>()
            .ConstructUsing(command => new ToDoItem(command.Title, command.Status.Adapt<DAL.Entities.ItemStatus>())
            {
                Id = command.Id
            });
    }
}