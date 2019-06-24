using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Infrastructure.Data.MongoDb;
using JomMalaysia.Infrastructure.Data.MongoDb.Repositories;

namespace JomMalaysia.Infrastructure
{
    public class InfrastructureModule : Autofac.Module
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MongoDbContext>()
                .As<MongoDbContext>()
                .WithParameter("connectionString", ConnectionString)
                .WithParameter("databaseName", DatabaseName)
                .SingleInstance();


            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(repo => repo.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
