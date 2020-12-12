using Carter.OpenApi;
using CarterDemo.Models;

namespace CarterDemo.OpenApi
{
    public class GetToDoItemById : RouteMetaData
    {
        public override string Description { get; } = "Gets a ToDo item by id";

        public override string Tag { get; } = "ToDo";

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse()
            {
                Code = 200,
                Description = "A ToDoItem",
                Response = typeof(ToDoItem)
            },
            new RouteMetaDataResponse()
            {
                Code = 404,
                Description = "ToDoItem not found"
            }
        };
    }
}
