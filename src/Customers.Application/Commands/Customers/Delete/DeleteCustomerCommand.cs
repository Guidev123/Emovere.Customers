using Emovere.SharedKernel.Abstractions;

namespace Customers.Application.Commands.Customers.Delete
{
    public record DeleteCustomerCommand(Guid UserId) : Command<DeleteCustomerResponse>;
}