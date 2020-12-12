using Carter;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarterDemo.Negotiation
{
    public class XmlResponseNegotiator : IResponseNegotiator
    {
        public XmlResponseNegotiator()
        {
        }

        public bool CanHandle(MediaTypeHeaderValue accept)
        {
            return accept.MediaType.ToString().IndexOf("application/xml", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public async Task Handle(HttpRequest req, HttpResponse res, object model, CancellationToken cancellationToken)
        {
            res.ContentType = "application/xml; charset=utf-8";

            var serializer = new XmlSerializer(model.GetType());
            using (var writer = new MemoryStream())
            {
                serializer.Serialize(writer, model);
                await res.Body.WriteAsync(writer.ToArray(), cancellationToken);
            }
        }
    }
}
