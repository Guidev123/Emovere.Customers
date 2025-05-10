using Customers.Domain.Enums;
using Emovere.SharedKernel.Responses;
using System.Text.Json;

namespace Customers.API.Middlewares
{
    public class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var problemDetails = new
                {
                    Message = EReportMessages.INTERNAL_SERVER_ERROR.GetEnumDescription(),
                    IsSuccess = false,
                    Errors = new string[] { ex.Message }
                };

                string json = JsonSerializer.Serialize(problemDetails);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCode.INTERNAL_SERVER_ERROR_STATUS_CODE;

                logger.LogError("An internal error has occurred. Exception message: {Message}", ex.Message);

                await context.Response.WriteAsync(json);
            }
        }
    }
}