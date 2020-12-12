using Carter.OpenApi;
using CarterDemo.Models;
using System.Collections.Generic;

namespace CarterDemo.OpenApi
{
    public class GetToDoItems : RouteMetaData
    {
        public override string Description { get; } = "Returns a list of ToDo items";

        public override string Tag { get; } = "ToDo";

        public override RouteMetaDataResponse[] Responses { get; } =
        {
            new RouteMetaDataResponse()
            {
                Code = 200,
                Description = "A list of ToDoItem objects",
                Response = typeof(IEnumerable<ToDoItem>)
            }
        };
    }
}
