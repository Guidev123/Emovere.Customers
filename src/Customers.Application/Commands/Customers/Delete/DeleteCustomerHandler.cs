using Emovere.SharedKernel.Abstractions;
using Emovere.SharedKernel.Notifications;
using Emovere.SharedKernel.Responses;

namespace Customers.Application.Commands.Customers.Delete
{
    public sealed class DeleteCustomerHandler(INotificator notificator) : CommandHandler<DeleteCustomerCommand, DeleteCustomerResponse>(notificator)
    {
        public override Task<Response<DeleteCustomerResponse>> ExecuteAsync(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}