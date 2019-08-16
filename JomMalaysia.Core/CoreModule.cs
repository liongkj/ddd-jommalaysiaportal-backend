
using System.Reflection;
using System.Web.Http.Validation;
using Autofac;
using FluentValidation;
using FluentValidation.WebApi;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.UserUseCase;
using JomMalaysia.Core.UseCases.UserUseCase.Get.UseCase;
using JomMalaysia.Core.Validation;

namespace JomMalaysia.Core
{
    public class CoreModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<CreateMerchantUseCase>().As<ICreateMerchantUseCase>().InstancePerLifetimeScope();
            //builder.RegisterType<GetMerchantUseCase>().As<IGetMerchantUseCase>().InstancePerLifetimeScope();
            //builder.RegisterType<GetAllMerchantUseCase>().As<IGetAllMerchantUseCase>().InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(ThisAssembly)
            //      .Where(t => t.Name.EndsWith("Validator"))
            //      .AsImplementedInterfaces()
            //      .InstancePerLifetimeScope();

            //builder.RegisterType<FluentValidationModelValidatorProvider>().As<ModelValidatorProvider>();

            //builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();
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
