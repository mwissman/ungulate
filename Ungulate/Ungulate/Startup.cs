using Autofac;
using Owin;

namespace Ungulate
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<TopLevelModule>();
            var container = builder.Build();
        
            app.UseAutofacMiddleware(container);
        }
    }
}