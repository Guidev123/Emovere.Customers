using Customers.Domain.Entities;
using Customers.Domain.Interfaces;
using Customers.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Data.Repositories
{
    public sealed class CustomerRepository(CustomersDbContext context) : ICustomerRepository
    {
        public IUnitOfWork UnitOfWork => context;

        public void Create(Customer customer)
            => context.Customers.Add(customer);

        public async Task<Customer?> GetByDocumentAsync(Document document)
            => await context.Customers
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Document.Number == document.Number);

        public async Task<Customer?> GetByIdAsync(Guid id)
            => await context.Customers
                .AsNoTrackingWithIdentityResolution()
                .Include(c => c.Address)
                .FirstOrDefaultAsync(c => c.Id == id);

        public void Update(Customer customer)
            => context.Customers.Update(customer);

        public void Dispose()
            => context.Dispose();
    }
}