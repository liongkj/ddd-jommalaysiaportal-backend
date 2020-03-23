
using System.Reflection;
using Autofac;
using FluentValidation;
using JomMalaysia.Core.Indexes;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core
{
    public class CoreModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();

      
            builder.RegisterAssemblyTypes(dataAccess)
                   .Where(t => t.Name.EndsWith("UseCase"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
