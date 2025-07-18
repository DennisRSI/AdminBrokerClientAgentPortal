﻿using System;
using ClientPortal.Data;
using ClientPortal.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ClientPortal.Helpers;

namespace ClientPortal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            /*var hostBuilder = CreateWebHostBuilder(args);
            var host = hostBuilder.Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<Application1DbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    var dbInitializerLogger = services.GetRequiredService<ILogger<DbInitializer>>();
                    DbInitializer.Initialize(context, userManager, roleManager, dbInitializerLogger).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();*/
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseKestrel()
                .UseStartup<Startup>()
                
                .Build();

        /*public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options => {
                    options.Limits.MaxRequestLineSize = 100_000_000;
                    options.Limits.MaxRequestBufferSize = 100_000_000;
                    options.Limits.MaxRequestHeadersTotalSize = 100_000_000;
                });*/
    }
}
