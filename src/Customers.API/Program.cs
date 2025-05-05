using Customers.API.Configurations;

var builder = WebApplication
    .CreateBuilder(args)
    .AddCustomersDbContextConfiguration()
    .AddModelsConfiguration()
    .AddRepositoriesConfiguration()
    .AddApplicationServices()
    .AddMediatorHandlersConfiguration()
    .AddSecurityConfig()
    .AddEmailServicesConfiguration()
    .AddServicesConfiguration()
    .AddMessageBusConfiguration()
    .AddCustomMiddlewares()
    .AddNotificationConfiguration()
    .AddSwaggerConfig();

var app = builder
    .Build()
    .UseMiddlewares()
    .UseGrpcServices()
    .UseSwaggerConfig(builder)
    .UseApiSecurityConfig();

app.Run();