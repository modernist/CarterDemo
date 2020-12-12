using Carter.OpenApi;
using CarterDemo.Models;

namespace CarterDemo.OpenApi
{
    public class AddToDoItem : RouteMetaData
    {
        public override string Description { get; } = "Adds a new ToDo item";

        public override string Tag { get; } = "ToDo";

        public override RouteMetaDataRequest[] Requests { get; } =
        {
            new RouteMetaDataRequest()
            {
                Request = typeof(ToDoItem)
            }
        };

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse()
            {
                Code = 201,
                Description = "Created new ToDo item successfully",
                Response = typeof(ToDoItem)
            },
            new RouteMetaDataResponse()
            {
                Code = 400,
                Description = "Create request validation failed"
            },
            new RouteMetaDataResponse()
            {
                Code = 422,
                Description = "The item could not be created"
            }
        };
    }
}
