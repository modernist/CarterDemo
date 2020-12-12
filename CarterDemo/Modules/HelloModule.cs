using Carter;
using Carter.Request;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarterDemo.Modules
{
    public class HelloModule : CarterModule
    {
        public HelloModule()
        {
            Get("/api/hello", (req, res) =>
            {
                return res.WriteAsync("Hello from Carter");
            });

            Get("/api/hello/add/{a:int}/{b:int}", (req, res) =>
            {
                var a = req.RouteValues.As<int>("a");
                var b = req.RouteValues.As<int>("b");

                return res.WriteAsync($"{a} + {b} = {a + b}");
            });

            Post("/api/hello/add", (req, res) =>
            {
                var a = int.Parse(req.Form["a"].FirstOrDefault());
                var b = int.Parse(req.Form["b"].FirstOrDefault());
                return res.WriteAsync($"{a} + {b} = {a + b}");
            });

            Delete("/api/hello", (ctx) =>
            {
                ctx.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                return Task.CompletedTask;
            });
        }
    }
}
