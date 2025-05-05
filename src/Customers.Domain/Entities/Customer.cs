using Customers.Domain.Enums;
using Customers.Domain.ValueObjects;
using Emovere.SharedKernel.DomainObjects;

namespace Customers.Domain.Entities
{
    public class Customer : Entity, IAggregateRoot
    {
        private const int MIN_AGE = 16;

        public Customer(Guid id, string firstName, string lastName, string email, string document, DateTime birthDate)
        {
            Id = id;
            Name = new(firstName, lastName);
            Email = new(email);
            Document = new(document);
            BirthDate = birthDate;
            IsDeleted = false;
            Validate();
        }

        protected Customer()
        { }

        public Name Name { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public Document Document { get; private set; } = null!;
        public ProfileData? ProfileData { get; private set; }
        public Address? Address { get; private set; }
        public DateTime BirthDate { get; private set; }
        public bool IsDeleted { get; private set; }

        protected override void Validate()
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;

            if (BirthDate.Date > today.AddYears(-age)) age--;

            AssertionConcern.EnsureTrue(age >= MIN_AGE, $"Customer must be at least {MIN_AGE} years old.");
            AssertionConcern.EnsureNotNull(Name, EReportMessages.NAME_CANNOT_BE_EMPTY.GetEnumDescription());
            AssertionConcern.EnsureNotNull(Email, EReportMessages.EMAIL_CANNOT_BE_EMPTY.GetEnumDescription());
            AssertionConcern.EnsureNotNull(Document, EReportMessages.DOCUMENT_CANNOT_BE_EMPTY.GetEnumDescription());
            AssertionConcern.EnsureFalse(IsDeleted, EReportMessages.CUSTOMER_IS_DELETED.GetEnumDescription());
        }
    }
}