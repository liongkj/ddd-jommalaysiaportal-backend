﻿
using System.Reflection;
using System.Web.Http.Validation;
using Autofac;
using FluentValidation;
using FluentValidation.WebApi;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.ImageProcessingServices;
using JomMalaysia.Core.UseCases.UserUseCase;
using JomMalaysia.Core.UseCases.UserUseCase.Get.UseCase;
using JomMalaysia.Core.Validation;

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

            builder.RegisterType<ImageProcessor>()
                .As<IImageProcessor>()
                .InstancePerLifetimeScope();
            ;

            builder.RegisterAssemblyTypes(dataAccess)
                   .Where(t => t.Name.EndsWith("UseCase"))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
