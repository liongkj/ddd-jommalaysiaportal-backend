using System.Reflection;
using Autofac;
using JomMalaysia.Framework.WebServices;

namespace JomMalaysia.Framework
{
    public class FrameworkModule : Autofac.Module
    {
        //public string ConnectionString { get; set; }
        //public string DatabaseName { get; set; }
        protected override void Load(ContainerBuilder builder)
        {



            builder.RegisterType<WebServiceExecutor>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<WebServiceResponse>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<ApiBuilder>().AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}
