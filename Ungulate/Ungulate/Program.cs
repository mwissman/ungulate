using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using Owin;

namespace Ungulate
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9080/";

            
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Ungulate is running");
                Console.WriteLine("Press any key to close...");
                Console.ReadLine();
            }

        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<TopLevelModule>();
            var container = builder.Build();
            

            // Register the Autofac middleware FIRST.
            app.UseAutofacMiddleware(container);

            app.Use<FooMiddleWare>();
        }
    }

    public class FooMiddleWare: OwinMiddleware
    {
        public FooMiddleWare(OwinMiddleware next) : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            var path = context.Request.Path;

            context.Response.Write("path:"+path);

            await Next.Invoke(context);
        }
    }
}
