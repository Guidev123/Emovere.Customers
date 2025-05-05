using Customers.Domain.Entities;
using Customers.Domain.ValueObjects;

namespace Customers.Domain.Interfaces
{
    public interface ICustomerRepository : IDisposable
    {
        Task<Customer?> GetByIdAsync(Guid id);

        Task<Customer?> GetByDocumentAsync(Document document);

        void Create(Customer customer);

        void Update(Customer customer);

        IUnitOfWork UnitOfWork { get; }
    }
}