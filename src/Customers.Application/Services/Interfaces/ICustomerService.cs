using Customers.Application.Commands.Customers.Create;
using Emovere.SharedKernel.Responses;

namespace Customers.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Response<CreateCustomerResponse>> CreateCustomerAsync(CreateCustomerCommand command);
    }
}