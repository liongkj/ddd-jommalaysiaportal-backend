using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using JomMalaysia.Presentation.Scope;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.AspNetCore;
using JomMalaysia.Api.Providers;
using JomMalaysia.Infrastructure.Auth0.Mapping;
using JomMalaysia.Api.Scope;
using System.Collections.Generic;
using JomMalaysia.Core.Mapping;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

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
                options.Authority = Configuration["Auth0:Authority"];
                options.Audience = Configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier


                };
                options.EventsType = typeof(AppUserRoleValidation);

            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.MANAGER, policy => policy.RequireClaim(ClaimTypes.Role, "Manager"));
                options.AddPolicy(Policies.SUPERADMIN, policy => policy.RequireClaim(ClaimTypes.Role, "Superadmin"));
                options.AddPolicy(Policies.ADMIN, policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                options.AddPolicy(Policies.EDITOR, policy => policy.RequireClaim(ClaimTypes.Role, "Editor"));
            });
            //services.AddHttpContextAccessor();
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


            services.AddScoped<AppUserRoleValidation>();
            services.AddScoped<ILoginInfoProvider, AppUserRoleValidation>(opt => opt.GetRequiredService<AppUserRoleValidation>());


            //Add mongodb

            services.Configure<MongoSettings>(Configuration.GetSection(nameof(MongoDbContext)));
            services.AddSingleton<IMongoSettings>(sp => sp.GetRequiredService<IOptions<MongoSettings>>().Value);
            //services.AddSingleton<MerchantRepository>();

            //Add Mvc
            services.AddMvc(options =>
            {
                options.Filters.Add(new ApiExceptionFilterAttribute());

            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());

            //add swagger
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "JomMalaysiaAPI", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please enter into field the word 'Bearer' following by space and JWT Token",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                         { "Bearer", Enumerable.Empty<string>() }});

                });

            // Auto Mapper Configurations
            services.AddSingleton(new MapperConfiguration(mc =>
                 {
                     mc.AddProfile(new Auth0DataProfile());
                     mc.AddProfile(new DataProfile());
                     mc.AddProfile(new CoreDataProfile());
                 }).CreateMapper());

            // Now register our services with Autofac container.
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new InfrastructureModule());
            //builder.RegisterType<ClaimBasedLoginInfoProvider>().As<ILoginInfoProvider>().InstancePerLifetimeScope();
            //builder.RegisterType<AppSetting>().As<IAppSetting>().InstancePerLifetimeScope();

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
            // app.UseMiddleware(typeof(ExceptionHandlerSerializer));
            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
