using Autofac;
using Ungulate.Application;
using Ungulate.Domain;
using Ungulate.Infrastructure;

namespace Ungulate
{
    public class TopLevelModule :Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpStubMiddleWare>()
                .InstancePerLifetimeScope();

            builder.Register(c => new HttpResponseBuilder(c.Resolve<IMappingRepository>(), c.Resolve<IRequestHandlerBuilder>()))
                .InstancePerLifetimeScope()
                .As<IHttpResponseBuilder>();

            builder.Register(c => new RequestHandlerBuilder(c.Resolve<IMatcherBuilder>()))
                .As<IRequestHandlerBuilder>()
                .InstancePerLifetimeScope();

            builder.Register(c => new MappingRepository(c.Resolve<ISettings>()))
                .As<IMappingRepository>()
                .InstancePerLifetimeScope();

            builder.Register(c => new Settings())
                .As<ISettings>()
                .SingleInstance();

            builder.Register(c => new MatcherBuilder())
                .As<IMatcherBuilder>()
                .InstancePerLifetimeScope();
        }
    }
}