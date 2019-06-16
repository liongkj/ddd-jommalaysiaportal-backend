
using Autofac;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.UseCases;
using JomMalaysia.Core.UseCases.MerchantUseCase;

namespace JomMalaysia.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateMerchantUseCase>().As<ICreateMerchantUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<GetMerchantUseCase>().As<IGetMerchantUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<GetAllMerchantUseCase>().As<IGetAllMerchantUseCase>().InstancePerLifetimeScope();
        }
    }
}
