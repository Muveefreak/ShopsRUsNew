using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Api.Configuration
{
    public static class CorsServiceCollectionExtensions
    {
        public static IServiceCollection AddCorsRules(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Default", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            return services;
        }
    }
}
