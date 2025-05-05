using Customers.Application.Commands.Customers.Create;
using Customers.Domain.Entities;

namespace Customers.Application.Mappers
{
    public static class CustomerMappers
    {
        public static Customer MapToEntity(this CreateCustomerCommand command)
            => new(command.UserId, command.FirstName, command.LastName, command.Email, command.Document, command.BirthDate);
    }
}