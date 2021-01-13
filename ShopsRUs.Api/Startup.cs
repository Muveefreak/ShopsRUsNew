using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShopsRUs.Api.Configuration;
using ShopsRUs.Api.Filters;
using ShopsRUs.Core.Configuration;
using ShopsRUs.Core.Customers.Validators;
using ShopsRUs.Core.Discounts.Validators;
using ShopsRUs.Core.Orders.Validators;
using ShopsRUs.Infrastructure.Configuration;

namespace ShopsRUs.Api
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings.LoyaltyYears = Convert.ToInt32(_config["AppSettings:LoyaltyYears"]);
            AppSettings.ExemptedItems = _config["AppSettings:ExemptedItems"].ToLower().Split(',');

            services
                .AddCorsRules()
                .AddControllers()
                .AddNewtonsoftJson()
                ;

            services
                .AddMvc(options => { options.Filters.Add<ValidationFilter>(); })
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<CreateCustomerValidator>();
                    config.RegisterValidatorsFromAssemblyContaining<CreateOrderValidator>();
                    config.RegisterValidatorsFromAssemblyContaining<CreateDiscountValidator>();
                })
                ;

            services
                //.AddDatabase(_config.GetConnectionString("ShopsRUsConnectionString"))
                .AddDatabase(_config["ConnectionStrings:ShopsRUsConnectionString"])
                .AddCore(_config)
                .AddMediatR(typeof(CoreServiceCollectionExtensions).Assembly)
                //.AddRabbitMqMessageBroker(_config.GetSection("MessageBrokerSettings"))
                //.AddHangfireBackgroundJobServer(_config.GetSection("BackgroundJobServerSettings"))
                //.AddMessageBrokerCustomSubscriptions()
                //.AddMessageBrokerCustomPublishers()
                //.AddBackgroundProcessing(_config)
                ;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopsRUs MicroServices", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureSwagger();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app
                //.UseBackgroundJobServerDashboard()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                })
                ;
        }
    }
}
