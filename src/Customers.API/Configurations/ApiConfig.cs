using Customers.API.Configurations.Extensions;
using Customers.API.gRPC;
using Customers.API.Middlewares;
using Customers.Application.Services;
using Customers.Application.Services.Interfaces;
using Customers.Domain.Interfaces;
using Customers.Infrastructure.Data;
using Customers.Infrastructure.Data.Repositories;
using Emovere.Infrastructure.Bus;
using Emovere.Infrastructure.Email;
using Emovere.Infrastructure.EventSourcing;
using Emovere.SharedKernel.Abstractions.Mediator;
using Emovere.SharedKernel.Notifications;
using Microsoft.EntityFrameworkCore;
using MidR.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;
using System.Reflection;

namespace Customers.API.Configurations
{
    public static class ApiConfig
    {
        public static WebApplicationBuilder AddServicesConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddEventStoreConfiguration();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();
            builder.Services.AddGrpc();

            return builder;
        }

        public static WebApplicationBuilder AddModelsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<KeycloakExtension>(builder.Configuration.GetSection(nameof(KeycloakExtension)));

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

        public static WebApplicationBuilder AddEmailServicesConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddSendGrid(x =>
            {
                x.ApiKey = builder.Configuration.GetValue<string>("EmailSettings:ApiKey");
            });
            builder.Services.AddScoped<IEmailService, EmailService>();

            return builder;
        }

        public static WebApplicationBuilder AddCustomMiddlewares(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<GlobalExceptionMiddleware>();

            return builder;
        }

        public static WebApplicationBuilder AddNotificationConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<INotificator, Notificator>();

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
            Assembly.Load("Customers.Application");
            builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
            builder.Services.AddMidR("Customers.Application");

            return builder;
        }

        public static WebApplicationBuilder AddMessageBusConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddMessageBus(builder.Configuration.GetConnectionString("MessageBusConnection") ?? string.Empty);

            return builder;
        }

        public static WebApplication UseMiddlewares(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();

            return app;
        }

        public static WebApplication UseGrpcServices(this WebApplication app)
        {
            app.MapGrpcService<CustomerGrpcService>();

            return app;
        }
    }
}