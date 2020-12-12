using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Carter;
using Carter.Request;
using Carter.Response;
using CarterDemo.Models;
using Microsoft.AspNetCore.Http;

namespace CarterDemo.Modules
{
    public class ToDoModule : CarterModule
    {
        public ToDoModule(IToDoItemProvider provider) : base("/api/todo")
        {
            Get("/", (req, res) =>
            {
                var items = provider.GetItems();
                return res.AsJson(items);
            });

            Get("/{id:int:min(1)}", (req, res) =>
            {
                var id = req.RouteValues.As<int>("id");
                var item = provider.GetItem(id);
                if (item == null)
                {
                    res.StatusCode = (int)HttpStatusCode.NotFound;
                    return res.WriteAsync("Not found");
                }

                return res.Negotiate(item);
            });

            Get("/category/{category}", (req, res) =>
            {
                var category = req.RouteValues.As<string>("category");
                var items = provider.GetItems(item => string.Equals(item.Category, category, StringComparison.InvariantCultureIgnoreCase));

                return res.Negotiate(items);
            });
        }
    }
}
