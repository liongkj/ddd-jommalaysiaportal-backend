using System.Reflection;
using Autofac;
using JomMalaysia.Framework.Configuration;
using JomMalaysia.Presentation.Manager;
using JomMalaysia.Presentation.Scope;

namespace JomMalaysia.Presentation
{
    public class PresentationModule : Autofac.Module
    {
        //public string ConnectionString { get; set; }
        //public string DatabaseName { get; set; }
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<AuthorizationManagers>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<AppSetting>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<HasScopeHandler>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(gateway => gateway.Name.EndsWith("Gateway"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
