using FluentValidation;

namespace Customers.Application.Commands.Customers.Delete
{
    public sealed class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerValidator()
        {
        }
    }
}