
using System.Reflection;
using Autofac;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.UserUseCase;
using JomMalaysia.Core.UseCases.UserUseCase.Get.UseCase;

namespace JomMalaysia.Core
{
    public class CoreModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<CreateMerchantUseCase>().As<ICreateMerchantUseCase>().InstancePerLifetimeScope();
            //builder.RegisterType<GetMerchantUseCase>().As<IGetMerchantUseCase>().InstancePerLifetimeScope();
            //builder.RegisterType<GetAllMerchantUseCase>().As<IGetAllMerchantUseCase>().InstancePerLifetimeScope();

            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                   .Where(t => t.Name.EndsWith("UseCase"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
