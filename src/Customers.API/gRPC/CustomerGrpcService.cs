using Customers.Application.Commands.Customers.Create;
using Customers.Application.Services.Interfaces;
using Customers.Protos;
using Grpc.Core;
using CreateCustomerResponse = Customers.Protos.CreateCustomerResponse;

namespace Customers.API.gRPC
{
    public class CustomerGrpcService(ICustomerService customerService) : CustomerEndpoint.CustomerEndpointBase
    {
        public override async Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request, ServerCallContext context)
        {
            var result = await customerService.CreateCustomerAsync(MapToCommand(request));
            if (!result.IsSuccess)
            {
                return new()
                { IsSuccess = false };
            }

            return new()
            {
                IsSuccess = true,
                CustomerId = result.Data?.Id.ToString()
            };
        }

        private static CreateCustomerCommand MapToCommand(CreateCustomerRequest request)
            => new(
                Guid.Parse(request.UserId),
                request.FirstName,
                request.LastName,
                request.Email,
                request.Document,
                request.BirthDate.ToDateTime());
    }
}