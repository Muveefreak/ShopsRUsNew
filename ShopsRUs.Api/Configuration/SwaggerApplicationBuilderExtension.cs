using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Api.Configuration
{
    public static class SwaggerApplicationBuilderExtension
    {
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseSwagger();

            appBuilder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "ShopsRUs V1");
                c.RoutePrefix = string.Empty;
            });

            return appBuilder;
        }
    }
}
