using Customers.Domain.Entities;
using Customers.Domain.Interfaces;
using Emovere.Infrastructure.EventSourcing;
using Emovere.SharedKernel.Abstractions.Mediator;
using Emovere.SharedKernel.Events;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Data
{
    public sealed class CustomersDbContext(DbContextOptions<CustomersDbContext> options,
                                           IMediatorHandler mediatorHandler)
                                         : DbContext(options), IUnitOfWork
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersDbContext).Assembly);
        }

        public async Task<bool> CommitAsync()
        {
            await mediatorHandler.PublishEventsAsync(this);
            return await SaveChangesAsync() > 0;
        }
    }
}