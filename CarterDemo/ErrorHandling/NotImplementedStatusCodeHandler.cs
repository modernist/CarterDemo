using Carter;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace CarterDemo.ErrorHandling
{
    public class NotImplementedStatusCodeHandler : IStatusCodeHandler
    {
        public bool CanHandle(int statusCode)
        {
            return statusCode == (int)HttpStatusCode.NotImplemented;
        }

        public Task Handle(HttpContext ctx)
        {
            return ctx.Response.WriteAsync("Hold your horses!");
        }
    }
}
