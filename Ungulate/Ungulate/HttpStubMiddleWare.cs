using System.Threading.Tasks;
using Microsoft.Owin;
using Ungulate.Application;

namespace Ungulate
{
    public class HttpStubMiddleWare: OwinMiddleware
    {
        private readonly IHttpResponseBuilder _httpResponseBuilder;

        public HttpStubMiddleWare(OwinMiddleware next, IHttpResponseBuilder httpResponseBuilder) : base(next)
        {
            _httpResponseBuilder = httpResponseBuilder;
        }

        public async override Task Invoke(IOwinContext context)
        {
            var response = _httpResponseBuilder.Build(context.Request);

            response.ApplyTo(context.Response);

            await Next.Invoke(context);
        }
    }
}