using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Infrastructure.Configuration
{
    public static class DatabaseServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string dbConnectionString)
        {

            Seed.ConfigurationSettings = dbConnectionString;

            services
                .AddDbContext<ShopsRUsDbContext>(o => o.UseSqlServer(dbConnectionString));
            return services;
        }
    }
}
