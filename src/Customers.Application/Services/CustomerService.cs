using Customers.Application.Commands.Customers.Create;
using Customers.Application.Services.Interfaces;
using Emovere.SharedKernel.Abstractions.Mediator;
using Emovere.SharedKernel.Responses;

namespace Customers.Application.Services
{
    public sealed class CustomerService(IMediatorHandler mediatorHandler) : ICustomerService
    {
        public async Task<Response<CreateCustomerResponse>> CreateCustomerAsync(CreateCustomerCommand command)
            => await mediatorHandler.SendCommand(command).ConfigureAwait(false);
    }
}