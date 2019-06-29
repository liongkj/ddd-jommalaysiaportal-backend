using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
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
using JomMalaysia.Infrastructure.Data.MongoDb.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using JomMalaysia.Api.UseCases.Merchants;
using JomMalaysia.Api.UseCases.Merchants.GetMerchant;
using JomMalaysia.Api.UseCases.Merchants.CreateMerchant;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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


            //Add mongodb
            services.Configure<ApplicationDbContext>(Configuration.GetSection(nameof(ApplicationDbContext)));
            services.AddSingleton<IApplicationDbContext>(sp => sp.GetRequiredService<IOptions<ApplicationDbContext>>().Value);
            services.AddSingleton<MerchantRepository>();
            //Add Mvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //add swagger
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "JomMalaysiaAPI", Version = "v1" }));
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DataProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            // Now register our services with Autofac container.
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new InfrastructureModule());
            // Presenters
            builder.RegisterType<CreateMerchantPresenter>().SingleInstance();
            builder.RegisterType<GetMerchantPresenter>().SingleInstance();
            builder.RegisterType<GetAllMerchantPresenter>().SingleInstance();
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
