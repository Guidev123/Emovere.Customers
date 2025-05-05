using Customers.Domain.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Customers.Application.Commands.Customers.Create
{
    public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.UserId)
                .NotEqual(Guid.Empty)
                .WithMessage(EReportMessages.USER_ID_REQUIRED.GetEnumDescription());

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(EReportMessages.EMAIL_REQUIRED.GetEnumDescription())
                .EmailAddress()
                .WithMessage(EReportMessages.EMAIL_INVALID.GetEnumDescription());

            RuleFor(x => x.FirstName)
                .Must(HasOnlyLetters)
                .WithMessage(EReportMessages.FIRST_NAME_INVALID_CHARACTERS.GetEnumDescription())
                .NotEmpty()
                .WithMessage(EReportMessages.FIRST_NAME_REQUIRED.GetEnumDescription())
                .MaximumLength(50)
                .WithMessage(EReportMessages.FIRST_NAME_TOO_LONG.GetEnumDescription())
                .MinimumLength(2)
                .WithMessage(EReportMessages.FIRST_NAME_TOO_SHORT.GetEnumDescription());

            RuleFor(x => x.LastName)
                .Must(HasOnlyLetters)
                .WithMessage(EReportMessages.LAST_NAME_INVALID_CHARACTERS.GetEnumDescription())
                .NotEmpty()
                .WithMessage(EReportMessages.LAST_NAME_REQUIRED.GetEnumDescription())
                .MaximumLength(50)
                .WithMessage(EReportMessages.LAST_NAME_TOO_LONG.GetEnumDescription())
                .MinimumLength(2)
                .WithMessage(EReportMessages.LAST_NAME_TOO_SHORT.GetEnumDescription());

            RuleFor(x => x.Document)
                 .NotEmpty()
                 .WithMessage(EReportMessages.DOCUMENT_REQUIRED.GetEnumDescription())
                 .Must(DocumentIsValid)
                 .WithMessage(EReportMessages.DOCUMENT_INVALID.GetEnumDescription());

            RuleFor(x => x.BirthDate)
                .Must(IsValidAge)
                .WithMessage(string.Format(EReportMessages.INVALID_AGE.GetEnumDescription(), MIN_AGE));
        }

        private static bool IsValidAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age)) age--;

            return age >= MIN_AGE;
        }

        private static bool HasOnlyLetters(string input) => new Regex(@"^[a-zA-Z]+$").IsMatch(input);

        private const int DOCUMENT_MAX_LENGTH = 11;
        private const int MIN_AGE = 16;

        public static string JustNumbers(string input) => new(input.Where(char.IsDigit).ToArray());

        public static bool DocumentIsValid(string number)
        {
            number = JustNumbers(number);

            if (number.Length > DOCUMENT_MAX_LENGTH)
                return false;

            while (number.Length != DOCUMENT_MAX_LENGTH)
                number = '0' + number;

            var equal = true;
            for (var i = 1; i < DOCUMENT_MAX_LENGTH && equal; i++)
                if (number[i] != number[0])
                    equal = false;

            if (equal || number == "12345678909")
                return false;

            var numbers = new int[DOCUMENT_MAX_LENGTH];

            for (var i = 0; i < DOCUMENT_MAX_LENGTH; i++)
                numbers[i] = int.Parse(number[i].ToString());

            var sum = 0;
            for (var i = 0; i < 9; i++)
                sum += (10 - i) * numbers[i];

            var result = sum % DOCUMENT_MAX_LENGTH;

            if (result == 1 || result == 0)
            {
                if (numbers[9] != 0)
                    return false;
            }
            else if (numbers[9] != DOCUMENT_MAX_LENGTH - result)
                return false;

            sum = 0;
            for (var i = 0; i < 10; i++)
                sum += (DOCUMENT_MAX_LENGTH - i) * numbers[i];

            result = sum % DOCUMENT_MAX_LENGTH;

            if (result == 1 || result == 0)
            {
                if (numbers[10] != 0)
                    return false;
            }
            else if (numbers[10] != DOCUMENT_MAX_LENGTH - result)
                return false;

            return true;
        }
    }
}