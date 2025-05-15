using Customers.API.Configurations;
using Emovere.WebApi.Config;

var builder = WebApplication
    .CreateBuilder(args)
    .AddSharedConfig()
    .AddCustomersDbContextConfiguration()
    .AddModelsConfiguration()
    .AddRepositoriesConfiguration()
    .AddBackgroundServices()
    .AddApplicationServices()
    .AddMediatorHandlersConfiguration()
    .AddServicesConfiguration()
    .AddSwaggerConfig();

var app = builder
    .Build()
    .UseApiSecurityConfig()
    .UseSerilogSettings()
    .UseMiddlewares()
    .UseGrpcServices()
    .UseSwaggerConfig();

app.Run();