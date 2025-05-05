using Customers.Domain.Entities;
using Customers.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.Data.Mappings
{
    public sealed class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Customers");

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(x => x.FirstName)
                     .IsRequired()
                     .HasColumnType("varchar(50)")
                     .HasColumnName("FirstName");

                name.Property(x => x.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasColumnName("LastName");
            });

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(x => x.Address)
                     .IsRequired()
                     .HasColumnType("varchar(100)")
                     .HasColumnName(nameof(Email));
            });

            builder.OwnsOne(x => x.Document, document =>
            {
                document.Property(x => x.Number)
                        .IsRequired()
                        .HasColumnType("varchar(14)")
                        .HasColumnName(nameof(Document));

                document.HasIndex(x => x.Number)
                        .IsUnique()
                        .HasDatabaseName("IX_Customer_Document");
            });

            builder.OwnsOne(x => x.ProfileData, profileData =>
            {
                profileData.Property(x => x.ImageUrl)
                           .HasColumnType("varchar(300)").HasColumnName("ImageUrl");

                profileData.Property(x => x.DisplayName)
                            .HasColumnType("varchar(50)").HasColumnName("DisplayName");
            });

            builder.Property(x => x.IsDeleted).IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.HasOne(x => x.Address).WithOne(x => x.Customer).OnDelete(DeleteBehavior.Cascade);
        }
    }
}