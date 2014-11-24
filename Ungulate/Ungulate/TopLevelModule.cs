using Autofac;

namespace Ungulate
{
    public class TopLevelModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpStubMiddleWare>()
                .InstancePerLifetimeScope();

            builder.RegisterType<HttpResponseBuilder>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
        }
    }
}