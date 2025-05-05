using System.ComponentModel;
using System.Reflection;

namespace Customers.Domain.Enums
{
    public enum EReportMessages
    {
        [Description("Error: Internal Server Error.")]
        INTERNAL_SERVER_ERROR,

        [Description("Error: Name cannot be empty.")]
        NAME_CANNOT_BE_EMPTY,

        [Description("Error: Email cannot be empty.")]
        EMAIL_CANNOT_BE_EMPTY,

        [Description("Error: Document cannot be empty.")]
        DOCUMENT_CANNOT_BE_EMPTY,

        [Description("Error: Customer is deleted.")]
        CUSTOMER_IS_DELETED,

        [Description("Error: Fail to persist Customer.")]
        CUSTOMER_NOT_PERSISTED,

        [Description("Error: User ID is required.")]
        USER_ID_REQUIRED,

        [Description("Error: Email is required.")]
        EMAIL_REQUIRED,

        [Description("Error: Email is invalid.")]
        EMAIL_INVALID,

        [Description("Error: First name is required.")]
        FIRST_NAME_REQUIRED,

        [Description("Error: First name is too short.")]
        FIRST_NAME_TOO_SHORT,

        [Description("Error: First name is too long.")]
        FIRST_NAME_TOO_LONG,

        [Description("Error: First name must contain only letters.")]
        FIRST_NAME_INVALID_CHARACTERS,

        [Description("Error: Last name is required.")]
        LAST_NAME_REQUIRED,

        [Description("Error: Last name is too short.")]
        LAST_NAME_TOO_SHORT,

        [Description("Error: Last name is too long.")]
        LAST_NAME_TOO_LONG,

        [Description("Error: Last name must contain only letters.")]
        LAST_NAME_INVALID_CHARACTERS,

        [Description("Error: Document is required.")]
        DOCUMENT_REQUIRED,

        [Description("Error: Document is invalid.")]
        DOCUMENT_INVALID,

        [Description("Error: Age must be at least {0} years.")]
        INVALID_AGE,

        [Description("Error: Street cannot be empty.")]
        STREET_CANNOT_BE_EMPTY,

        [Description("Error: Street must be between 5 and 100 characters.")]
        STREET_LENGTH_OUT_OF_RANGE,

        [Description("Error: Number cannot be empty.")]
        NUMBER_CANNOT_BE_EMPTY,

        [Description("Error: Number must be a valid number.")]
        NUMBER_INVALID_FORMAT,

        [Description("Error: Additional info must be up to 200 characters.")]
        ADDITIONAL_INFO_TOO_LONG,

        [Description("Error: Neighborhood cannot be empty.")]
        NEIGHBORHOOD_CANNOT_BE_EMPTY,

        [Description("Error: Neighborhood must be between 3 and 100 characters.")]
        NEIGHBORHOOD_LENGTH_OUT_OF_RANGE,

        [Description("Error: Zip code cannot be empty.")]
        ZIPCODE_CANNOT_BE_EMPTY,

        [Description("Error: Zip code must be in the format 00000-000.")]
        ZIPCODE_INVALID_FORMAT,

        [Description("Error: State cannot be empty.")]
        STATE_CANNOT_BE_EMPTY,

        [Description("Error: State must have exactly 2 characters.")]
        STATE_INVALID_LENGTH,

        [Description("Error: City cannot be empty.")]
        CITY_CANNOT_BE_EMPTY,

        [Description("Error: City must be between 3 and 100 characters.")]
        CITY_LENGTH_OUT_OF_RANGE,

        [Description("Error: Document already registered.")]
        DOCUMENT_ALREADY_REGISTERED,
    }

    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString()) ?? default!;

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[]
                ?? throw new ArgumentNullException();

            return attributes is not null && attributes.Length != 0 ? attributes.First().Description : value.ToString();
        }
    }
}