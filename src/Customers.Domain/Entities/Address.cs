using Customers.Domain.Enums;
using Emovere.SharedKernel.DomainObjects;

namespace Customers.Domain.Entities
{
    public class Address : Entity
    {
        public Address(Guid customerId, string street, string number,
                       string additionalInfo, string neighborhood,
                       string zipCode, string city, string state)
        {
            CustomerId = customerId;
            Street = street;
            Number = number;
            AdditionalInfo = additionalInfo;
            Neighborhood = neighborhood;
            ZipCode = zipCode;
            City = city;
            State = state;
            Validate();
        }

        private Address()
        { }

        public string Street { get; private set; } = string.Empty;
        public string Number { get; private set; } = string.Empty;
        public string AdditionalInfo { get; private set; } = string.Empty;
        public string Neighborhood { get; private set; } = string.Empty;
        public string ZipCode { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string State { get; private set; } = string.Empty;
        public Guid CustomerId { get; private set; }
        public Customer? Customer { get; private set; }

        protected override void Validate()
        {
            AssertionConcern.EnsureNotEmpty(Street, EReportMessages.STREET_CANNOT_BE_EMPTY.GetEnumDescription());
            AssertionConcern.EnsureLengthInRange(Street, 5, 100, EReportMessages.STREET_LENGTH_OUT_OF_RANGE.GetEnumDescription());

            AssertionConcern.EnsureNotEmpty(Number, EReportMessages.NUMBER_CANNOT_BE_EMPTY.GetEnumDescription());
            AssertionConcern.EnsureMatchesPattern(@"^\d+$", Number, EReportMessages.NUMBER_INVALID_FORMAT.GetEnumDescription());

            AssertionConcern.EnsureMaxLength(AdditionalInfo, 200, EReportMessages.ADDITIONAL_INFO_TOO_LONG.GetEnumDescription());

            AssertionConcern.EnsureNotEmpty(Neighborhood, EReportMessages.NEIGHBORHOOD_CANNOT_BE_EMPTY.GetEnumDescription());
            AssertionConcern.EnsureLengthInRange(Neighborhood, 3, 100, EReportMessages.NEIGHBORHOOD_LENGTH_OUT_OF_RANGE.GetEnumDescription());

            AssertionConcern.EnsureNotEmpty(ZipCode, EReportMessages.ZIPCODE_CANNOT_BE_EMPTY.GetEnumDescription());
            AssertionConcern.EnsureMatchesPattern(@"^\d{5}-\d{3}$", ZipCode, EReportMessages.ZIPCODE_INVALID_FORMAT.GetEnumDescription());

            AssertionConcern.EnsureNotEmpty(State, EReportMessages.STATE_CANNOT_BE_EMPTY.GetEnumDescription());
            AssertionConcern.EnsureLengthInRange(State, 2, 2, EReportMessages.STATE_INVALID_LENGTH.GetEnumDescription());

            AssertionConcern.EnsureNotEmpty(City, EReportMessages.CITY_CANNOT_BE_EMPTY.GetEnumDescription());
            AssertionConcern.EnsureLengthInRange(City, 3, 100, EReportMessages.CITY_LENGTH_OUT_OF_RANGE.GetEnumDescription());
        }
    }
}