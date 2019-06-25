using System.Reflection;
using Autofac;
using JomMalaysia.Infrastructure.Data.MongoDb;

namespace JomMalaysia.Infrastructure
{
    public class InfrastructureModule : Autofac.Module
    {
        //public string ConnectionString { get; set; }
        //public string DatabaseName { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDbContext>()
                .AsImplementedInterfaces()
                .SingleInstance();


            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(repo => repo.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
