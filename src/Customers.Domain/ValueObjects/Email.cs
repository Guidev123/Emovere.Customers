using Emovere.SharedKernel.DomainObjects;

namespace Customers.Domain.ValueObjects
{
    public record Email : ValueObject
    {
        public string Address { get; }

        public Email(string address)
        {
            Address = address;
            Validate();
        }

        protected override void Validate()
            => AssertionConcern.EnsureMatchesPattern(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", Address, "Invalid email format.");
    }
}