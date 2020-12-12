using Carter;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;

namespace CarterDemo.ErrorHandling
{
    public class ErrorModule : CarterModule
    {
        public ErrorModule()
        {
            Get("/error", (req, res) => { throw new Exception("oops"); });

            Get("/errorhandler", (req, res) =>
            {
                var error = string.Empty;
                var feature = req.HttpContext.Features.Get<IExceptionHandlerFeature>();
                if (feature != null)
                {
                    error = feature.Error.ToString();
                }

                return res.WriteAsync($"There has been an error{Environment.NewLine}{error}");
            });
        }
    }
}