using Carter;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;

namespace CarterDemo.Modules
{
    public class HooksModule : CarterModule
    {
        public HooksModule() : base("/api/hooks")
        {
            this.Before = async (context) =>
            {
                if (!context.Request.Headers.TryGetValue("x-custom-header", out var values) ||
                    !values.Contains("let-me-in", StringComparer.InvariantCulture))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    await context.Response.WriteAsync("Required header missing");
                    return false;
                }

                return true;
            };

            Get("/", (req, res) => res.WriteAsync("Got here"));

            this.After = context => context.Response.WriteAsync("Ended up here");
        }
    }
}
