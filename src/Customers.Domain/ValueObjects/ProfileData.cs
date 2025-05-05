using Emovere.SharedKernel.DomainObjects;

namespace Customers.Domain.ValueObjects
{
    public record ProfileData : ValueObject
    {
        public ProfileData(string imageUrl, string displayName)
        {
            ImageUrl = imageUrl;
            DisplayName = displayName;
            Validate();
        }

        public string ImageUrl { get; }
        public string DisplayName { get; }

        protected override void Validate()
        {
            AssertionConcern.EnsureNotEmpty(ImageUrl, "Image URL cannot be empty.");
            AssertionConcern.EnsureNotEmpty(DisplayName, "Display name cannot be empty.");
            AssertionConcern.EnsureLengthInRange(DisplayName, 2, 50, "Display name must be between 2 and 50 characters.");
            AssertionConcern.EnsureMatchesPattern(@"^[a-zA-Z0-9\s]+$", DisplayName, "Display name can only contain letters, numbers, and spaces.");
        }
    }
}