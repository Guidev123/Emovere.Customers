using Customers.API.Configurations.Extensions;
using Customers.API.gRPC;
using Customers.Application.Services;
using Customers.Application.Services.Interfaces;
using Customers.Domain.Interfaces;
using Customers.Infrastructure.BackgroundServices;
using Customers.Infrastructure.Data;
using Customers.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using MidR.DependencyInjection;
using System.Reflection;

namespace Customers.API.Configurations
{
    public static class ApiConfig
    {
        private const string HANDLER_ASSEMBLY_NAME = "Customers.Application";

        public static WebApplicationBuilder AddServicesConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddMemoryCache();
            builder.Services.AddGrpc();

            return builder;
        }

        public static WebApplicationBuilder AddModelsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<KeycloakExtension>(builder.Configuration.GetSection(nameof(KeycloakExtension)));

            return builder;
        }

        public static WebApplicationBuilder AddBackgroundServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHostedService<DeletedUserIntegrationEventHandler>();

            return builder;
        }

        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICustomerService, CustomerService>();

            return builder;
        }

        public static WebApplicationBuilder AddRepositoriesConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            return builder;
        }

        public static WebApplicationBuilder AddCustomersDbContextConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<CustomersDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

            return builder;
        }

        public static WebApplicationBuilder AddMediatorHandlersConfiguration(this WebApplicationBuilder builder)
        {
            Assembly.Load(HANDLER_ASSEMBLY_NAME);
            builder.Services.AddMidR(HANDLER_ASSEMBLY_NAME);

            return builder;
        }

        public static WebApplication UseGrpcServices(this WebApplication app)
        {
            app.MapGrpcService<CustomerGrpcService>();

            return app;
        }
    }
}