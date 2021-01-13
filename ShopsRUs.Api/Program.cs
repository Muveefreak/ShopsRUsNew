using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShopsRUs.Infrastructure;

namespace ShopsRUs.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            //var getConn = config.GetConnectionString("ShopsRUsConnectionString");

            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ShopsRUsDbContext>();
                
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError($"****About to seed Data******");
                await Seed.SeedCustomers(context);
                await Seed.SeedDiscounts(context);
                await Seed.SeedOrders(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, $"An error occurred during migration");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
