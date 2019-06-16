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
            //Add Auth0
            services.Configure<CookiePolicyOptions>(options =>
            {
                //auth0
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect("Auth0", options => SetOpenIdConnectOptions(options));
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
        private void SetOpenIdConnectOptions(OpenIdConnectOptions options)
        {
            options.Authority = $"https://{Configuration["Auth0:Domain"]}";

            // Configure the Auth0 Client ID and Client Secret
            options.ClientId = Configuration["Auth0:ClientId"];
            options.ClientSecret = Configuration["Auth0:ClientSecret"];

            // Configure the scope
            options.Scope.Clear();
            options.Scope.Add("openid");

            // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
            // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
            options.CallbackPath = new PathString("/callback");

            // Configure the Claims Issuer to be Auth0
            options.ClaimsIssuer = "Auth0";

            options.Events = new OpenIdConnectEvents
            {
                OnRedirectToIdentityProvider = context =>
                {
                    context.ProtocolMessage.SetParameter("audience", "https://auth");

                    return Task.FromResult(0);
                }
            };

            options.Events = new OpenIdConnectEvents
            {
                // handle the logout redirection
                OnRedirectToIdentityProviderForSignOut = (context) =>
                {
                    var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

                    var postLogoutUri = context.Properties.RedirectUri;
                    if (!string.IsNullOrEmpty(postLogoutUri))
                    {
                        if (postLogoutUri.StartsWith("/"))
                        {
                            // transform to absolute
                            var request = context.Request;
                            postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                        }
                        logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                    }

                    context.Response.Redirect(logoutUri);
                    context.HandleResponse();

                    return Task.CompletedTask;
                }
            };
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
            //app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
