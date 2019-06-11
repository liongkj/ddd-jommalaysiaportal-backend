
using Autofac;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases;

namespace JomMalaysia.Core
{
    public class CoreModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateMerchantUseCase>().As<ICreateMerchantUseCase>().InstancePerLifetimeScope();
            builder.RegisterType<GetAllMerchantUseCase>().As<IGetAllMerchantUseCase>().InstancePerLifetimeScope();
        }
    }
}
