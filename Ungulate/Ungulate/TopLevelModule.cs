using Autofac;

namespace Ungulate
{
    public class TopLevelModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FooMiddleWare>()
                .InstancePerLifetimeScope();

            builder.RegisterType<Thing>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
        }
    }
}