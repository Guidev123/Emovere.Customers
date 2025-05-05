using Customers.Application.Mappers;
using Customers.Domain.Enums;
using Customers.Domain.Interfaces;
using Emovere.SharedKernel.Abstractions;
using Emovere.SharedKernel.Notifications;
using Emovere.SharedKernel.Responses;

namespace Customers.Application.Commands.Customers.Create
{
    public sealed class CreateCustomerHandler(INotificator notificator,
                                              ICustomerRepository customerRepository)
                                            : CommandHandler<CreateCustomerCommand, CreateCustomerResponse>(notificator)
    {
        public override async Task<Response<CreateCustomerResponse>> ExecuteAsync(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!ExecuteValidation(new CreateCustomerValidator(), request))
                return Response<CreateCustomerResponse>.Failure(Notifications);

            var existentCustomer = await customerRepository.GetByDocumentAsync(new(request.Document));
            if (existentCustomer is not null)
            {
                Notify(EReportMessages.DOCUMENT_ALREADY_REGISTERED.GetEnumDescription());
                return Response<CreateCustomerResponse>.Failure(Notifications);
            }

            customerRepository.Create(request.MapToEntity());

            if (!await customerRepository.UnitOfWork.CommitAsync())
            {
                Notify(EReportMessages.CUSTOMER_NOT_PERSISTED.GetEnumDescription());
                return Response<CreateCustomerResponse>.Failure(Notifications);
            }

            return Response<CreateCustomerResponse>.Success(new(request.UserId), code: StatusCode.CREATED_STATUS_CODE);
        }
    }
}