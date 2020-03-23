using System;
using System.Reflection;
using System.Security.Claims;
using Autofac;
using AutoMapper;
using FluentValidation.AspNetCore;
using JomMalaysia.Api.Providers;
using JomMalaysia.Api.Scope;
using JomMalaysia.Core;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Mapping;
using JomMalaysia.Infrastructure;
using JomMalaysia.Infrastructure.Algolia;
using JomMalaysia.Infrastructure.Auth0.Mapping;
using JomMalaysia.Infrastructure.Data.Mapping;
using JomMalaysia.Infrastructure.Data.MongoDb;
using JomMalaysia.Presentation.Scope;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

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
        public void ConfigureServices(IServiceCollection services)
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
            
            //add algolia
            
            services.Configure<AlgoliaSetting>(Configuration.GetSection(nameof(AlgoliaClient)));
            services.AddSingleton<IAlgoliaSetting>(sp => sp.GetRequiredService<IOptions<AlgoliaSetting>>().Value);
            //services.AddSingleton<MerchantRepository>();

            //Add Mvc
            services.AddResponseCaching();

            services.AddRazorPages();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new ApiExceptionFilterAttribute());

            }).AddNewtonsoftJson(options => options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            //add swagger
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JomMalaysiaAPI", Version = "v1" });

                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter into field the word 'Bearer' following by space and JWT Token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    });
                });

            // Auto Mapper Configurations
            services.AddSingleton(new MapperConfiguration(mc =>
                 {
                     mc.AddProfile(new Auth0DataProfile());
                     mc.AddProfile(new DataProfile());
                     mc.AddProfile(new CoreDataProfile());
                     mc.AddProfile(new AlgoliaDataProfile());
                     
                 }).CreateMapper());

            //builder.RegisterType<ClaimBasedLoginInfoProvider>().As<ILoginInfoProvider>().InstancePerLifetimeScope();
            //builder.RegisterType<AppSetting>().As<IAppSetting>().InstancePerLifetimeScope();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {

            // Use and configure Autofac
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("Presenter")).SingleInstance();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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
            app.UseRouting();
            app.UseResponseCaching();
            app.Use(async (context, next) =>
            {
                context.Response.GetTypedHeaders().CacheControl =
                    new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromSeconds(10)
                    };
                context.Response.Headers[HeaderNames.Vary] =
                    new[] { "Accept-Encoding" };

                var responseCachingFeature = context.Features.Get<IResponseCachingFeature>();

                if (responseCachingFeature != null)
                {
                    responseCachingFeature.VaryByQueryKeys = new[] { "*" };
                }
                await next();
            });
            app.UseAuthorization();
            app.UseEndpoints(x =>
            {
                x.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                x.MapRazorPages();
            });
        }
    }
}
