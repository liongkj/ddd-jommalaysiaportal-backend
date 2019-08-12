﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using JomMalaysia.Core;
using JomMalaysia.Infrastructure;
using JomMalaysia.Infrastructure.Data.Mapping;
using JomMalaysia.Infrastructure.Data.MongoDb;
using JomMalaysia.Core.Interfaces;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using JomMalaysia.Api.UseCases.Merchants;
using JomMalaysia.Api.UseCases.Merchants.GetMerchant;
using JomMalaysia.Api.UseCases.Merchants.CreateMerchant;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using JomMalaysia.Presentation.Scope;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.AspNetCore;
using JomMalaysia.Core.Validation;
using JomMalaysia.Core.UseCases.UserUseCase;
using JomMalaysia.Core.UseCases.UserUseCase.Get.UseCase;
using JomMalaysia.Api.Providers;
using System.Security.Claims;
using JomMalaysia.Infrastructure.Auth0.Mapping;
using JomMalaysia.Framework.Configuration;
using FluentValidation.WebApi;
using System.Web.Http;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using FluentValidation;

namespace JomMalaysia.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            // Add Authentication Services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://jomn9.auth0.com/";
                options.Audience = "https://localhost:44368/";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:merchant", policy => policy.Requirements.Add(new HasScopeRequirement("read:merchant", "https://jomn9.auth0.com/")));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            //Add mongodb

            services.Configure<MongoSettings>(Configuration.GetSection(nameof(MongoDbContext)));
            services.AddSingleton<IMongoSettings>(sp => sp.GetRequiredService<IOptions<MongoSettings>>().Value);
            //services.AddSingleton<MerchantRepository>();

            //Add Mvc
            services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateListingRequestValidator>());
            //fluent validation service moved to core module

            //add swagger
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "JomMalaysiaAPI", Version = "v1" }));

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DataProfile());
                mc.AddProfile(new Mapping.DataProfile());
                mc.AddProfile(new Auth0DataProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Now register our services with Autofac container.
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterType<ClaimBasedLoginInfoProvider>().As<ILoginInfoProvider>().InstancePerLifetimeScope();
            builder.RegisterType<AppSetting>().As<IAppSetting>().InstancePerLifetimeScope();

            // Presenters
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Presenter")).SingleInstance();
            builder.Populate(services);
            var container = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JomMalaysia API v1");
                c.RoutePrefix = string.Empty;
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
