using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Core.Orders.Interfaces;
using ShopsRUs.Core.Orders.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Core.Configuration
{
    public static class CoreServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection container, IConfiguration config)
        {
            var settings = new ShopsRUsSettings();



            config.Bind(settings);

            container
                //Domain Level Validation
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>))
                .AddScoped<IOrderService, OrderService>();
            ;

            return container;
        }
    }
}
