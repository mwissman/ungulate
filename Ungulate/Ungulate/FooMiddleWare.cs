using System.Threading.Tasks;
using Microsoft.Owin;

namespace Ungulate
{
    public class FooMiddleWare: OwinMiddleware
    {
        private readonly IThing _thing;

        public FooMiddleWare(OwinMiddleware next, IThing thing) : base(next)
        {
            _thing = thing;
        }

        public async override Task Invoke(IOwinContext context)
        {
            var path = context.Request.Path;

            context.Response.Write("path:"+path);
            context.Response.Write("Uri:"+context.Request.Uri.ToString());
            context.Response.Write("thing: "+_thing.Stuff());

            await Next.Invoke(context);
        }
    }
}