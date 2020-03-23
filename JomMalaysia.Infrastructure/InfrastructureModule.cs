using System.Reflection;
using Autofac;
using JomMalaysia.Infrastructure.Algolia;
using JomMalaysia.Infrastructure.Auth0;
using JomMalaysia.Infrastructure.Auth0.Managers;
using JomMalaysia.Infrastructure.Data.MongoDb;

namespace JomMalaysia.Infrastructure
{
    public class InfrastructureModule : Autofac.Module
    {
        //public string ConnectionString { get; set; }
        //public string DatabaseName { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AlgoliaClient>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<MongoDbContext>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<Auth0Setting>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<Auth0AccessTokenManager>()
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(repo => repo.Name.EndsWith("Indexer"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(repo => repo.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
