using System;
using System.Text;
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
            try
            {
                var response = _httpResponseBuilder.Build(context.Request);

                response.ApplyTo(context.Response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                string message = e.ToString();
                context.Response.Body.Write(Encoding.ASCII.GetBytes(message), 0, message.Length);
            }

            if (Next != null)
            {
                await Next.Invoke(context);
            }
        }
    }
}