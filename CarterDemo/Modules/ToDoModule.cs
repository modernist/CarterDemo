using Carter;
using Carter.ModelBinding;
using Carter.Request;
using Carter.Response;
using CarterDemo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using CarterDemo.OpenApi;

namespace CarterDemo.Modules
{
    public class ToDoModule : CarterModule
    {
        public ToDoModule(IToDoItemProvider provider) : base("/api/todo")
        {
            Get<GetToDoItems>("/", (req, res) =>
            {
                var items = provider.GetItems();
                return res.AsJson(items);
            });

            Get<GetToDoItemById>("/{id:int:min(1)}", (req, res) =>
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

            Post<AddToDoItem>("/", async (req, res) =>
            {
                var request = await req.BindAndValidate<ToDoItem>();

                if (!request.ValidationResult.IsValid)
                {
                    res.StatusCode = 400;
                    await res.Negotiate(request.ValidationResult.GetFormattedErrors());
                    return;
                }

                var item = request.Data;
                var result = provider.AddItem(item);

                if (!result)
                {
                    res.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    return;
                }

                res.StatusCode = (int)HttpStatusCode.Created;
                await res.Negotiate(item);
            });

            Put("/{id:int}", async (req, res) =>
            {
                var (validationResult, item) = await req.BindAndValidate<ToDoItem>();

                if (!validationResult.IsValid)
                {
                    res.StatusCode = 400;
                    await res.Negotiate(validationResult.GetFormattedErrors());
                    return;
                }

                var id = req.RouteValues.As<int>("id");
                var (success, updatedItem) = provider.UpdateItem(id, item);

                if (!success)
                {
                    res.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    return;
                }

                res.StatusCode = (int)HttpStatusCode.OK;
                await res.Negotiate(updatedItem);
            });

            Delete("/{id:int}", (req, res) =>
            {
                var id = req.RouteValues.As<int>("id");

                if (!provider.DeleteItem(id))
                {
                    res.StatusCode = (int)HttpStatusCode.NotFound;
                }
                else
                {
                    res.StatusCode = (int)HttpStatusCode.NoContent;
                }
                return Task.CompletedTask;
            });
        }
    }
}
