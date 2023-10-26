using System.ComponentModel.DataAnnotations;
using Talpa_10_WebApp.RequestModels;

namespace Talpa_10_WebApp.Validations
{
    public class SuggestionCount : ValidationAttribute
    {

        public string GetErrorMessageMax() =>
        $"Er kunnen maximaal 3 suggesties worden gekozen!";

        public string GetErrorMessageMin() =>
        $"Er moet minimaal 1 suggestie worden gekozen!";

        protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
        {
            OutingRequest outingRequest = (OutingRequest)validationContext.ObjectInstance;

            if (outingRequest.SelectedSuggestionIds?.Count > 3)
            {
                return new ValidationResult(GetErrorMessageMax());
            }
            if (outingRequest.SelectedSuggestionIds?.Count < 1)
            {
                return new ValidationResult(GetErrorMessageMin());
            }

            return ValidationResult.Success;
        }
    }
}
