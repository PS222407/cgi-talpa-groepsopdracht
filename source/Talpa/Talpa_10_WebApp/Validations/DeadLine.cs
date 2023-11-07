using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Talpa_10_WebApp.RequestModels;

namespace Talpa_10_WebApp.Validations;

public class SuggestionCount : ValidationAttribute
{
    private static IStringLocalizer? _localizer;
    
    private string GetErrorMessage(ValidationContext validationContext)
    {
        return GetLocalizer(validationContext)[ErrorMessage];
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        OutingRequest outingRequest = (OutingRequest)validationContext.ObjectInstance;

        if (outingRequest.SelectedSuggestionIds?.Count > 3)
        {
            return new ValidationResult(GetErrorMessage(validationContext));
        }

        return ValidationResult.Success;
    }
    
    private IStringLocalizer GetLocalizer(ValidationContext validationContext)
    {
        if (_localizer is null)
        {
            IStringLocalizerFactory factory = validationContext.GetRequiredService<IStringLocalizerFactory>();
            IOptions<MvcDataAnnotationsLocalizationOptions> annotationOptions = validationContext.GetRequiredService<IOptions<MvcDataAnnotationsLocalizationOptions>>();
            _localizer = annotationOptions.Value.DataAnnotationLocalizerProvider(validationContext.ObjectType, factory);
        }

        return _localizer;
    }
}