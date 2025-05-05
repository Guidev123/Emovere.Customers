using Emovere.SharedKernel.Abstractions;

namespace Customers.Application.Commands.Customers.Create
{
    public record CreateCustomerCommand(
        Guid UserId, string FirstName,
        string LastName, string Email,
        string Document, DateTime BirthDate)
        : Command<CreateCustomerResponse>;
}