using System.ComponentModel.DataAnnotations;
using Talpa_10_WebApp.RequestModels;

namespace Talpa_10_WebApp.Validations;

public class SuggestionCount : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        OutingRequest outingRequest = (OutingRequest)validationContext.ObjectInstance;

        if (outingRequest.SelectedSuggestionIds?.Count > 3)
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        return ValidationResult.Success;
    }
}