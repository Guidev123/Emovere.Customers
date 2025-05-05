using Emovere.SharedKernel.DomainObjects;

namespace Customers.Domain.ValueObjects
{
    public record Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Validate();
        }

        public string FirstName { get; } = string.Empty;
        public string LastName { get; } = string.Empty;

        protected override void Validate()
        {
            AssertionConcern.EnsureDifferent(FirstName, LastName, "First name and last name cannot be the same.");
            AssertionConcern.EnsureNotEmpty(FirstName, "First name cannot be empty.");
            AssertionConcern.EnsureLengthInRange(FirstName, 2, 50, "First name must be between 2 and 50 characters.");
            AssertionConcern.EnsureNotEmpty(LastName, "Last name cannot be empty.");
            AssertionConcern.EnsureLengthInRange(LastName, 2, 50, "Last name must be between 2 and 50 characters.");
            AssertionConcern.EnsureMatchesPattern(@"^[a-zA-Z]+$", FirstName, "First name can only contain letters.");
            AssertionConcern.EnsureMatchesPattern(@"^[a-zA-Z]+$", LastName, "Last name can only contain letters.");
        }
    }
}