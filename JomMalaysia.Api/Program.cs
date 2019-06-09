﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace JomMalaysia.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
             .ConfigureAppConfiguration((builderContext, config) =>
             {
                 IHostingEnvironment env = builderContext.HostingEnvironment;
                
                 config.AddEnvironmentVariables();

             });

    }
}
