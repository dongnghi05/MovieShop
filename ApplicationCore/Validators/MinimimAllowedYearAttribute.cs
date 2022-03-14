using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Validators
{
    public class MinimimAllowedYearAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // get the user entered value
            var userEnetredYear = ((DateTime)value).Year;

            if (userEnetredYear < 1900)
            {
                return new ValidationResult("Year should be no less than 1900");
            }
            return ValidationResult.Success;
        }
    }
}