using Customers.API.Configurations;

var builder = WebApplication
    .CreateBuilder(args)
    .AddSerilog()
    .AddCustomersDbContextConfiguration()
    .AddModelsConfiguration()
    .AddRepositoriesConfiguration()
    .AddBackgroundServices()
    .AddApplicationServices()
    .AddMediatorHandlersConfiguration()
    .AddEmailServicesConfiguration()
    .AddServicesConfiguration()
    .AddMessageBusConfiguration()
    .AddCustomMiddlewares()
    .AddNotificationConfiguration()
    .AddSwaggerConfig();

var app = builder
    .Build()
    .UseSerilogSettings()
    .UseMiddlewares()
    .UseGrpcServices()
    .UseSwaggerConfig()
    .UseSecurity();

app.Run();