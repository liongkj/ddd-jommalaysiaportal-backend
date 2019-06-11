using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Infrastructure.Data.MongoDb;
using JomMalaysia.Infrastructure.Data.MongoDb.Repositories;

namespace JomMalaysia.Infrastructure
{
    public class InfrastructureModule : Module
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>()
                .As<ApplicationDbContext>()
                .WithParameter("connectionString", ConnectionString)
                .WithParameter("databaseName", DatabaseName)
                .SingleInstance();

            builder.RegisterType<MerchantRepository>().As<IMerchantRepository>().InstancePerLifetimeScope();
           
        }
    }
}
