using System.Threading.Tasks;
using Microsoft.Owin;

namespace Ungulate
{
    public class HttpStubMiddleWare: OwinMiddleware
    {
        private readonly IThing _thing;

        public HttpStubMiddleWare(OwinMiddleware next, IThing thing) : base(next)
        {
            _thing = thing;
        }

        public async override Task Invoke(IOwinContext context)
        {
            var path = context.Request.Path;

            context.Response.Write("path:"+path);
            context.Response.Write("Uri:"+context.Request.Uri.ToString());
            context.Response.Write("thing: "+_thing.Stuff());
           // context.Response.


            await Next.Invoke(context);
        }
    }
}