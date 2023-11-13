namespace Talpa_10_WebApp.Validations;

using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class StringListNotEmpty : ValidationAttribute
{
    private static IStringLocalizer? _localizer;

    private string GetErrorMessage(ValidationContext validationContext)
    {
        return GetLocalizer(validationContext)[ErrorMessage];
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is List<string> stringList)
        {
            if (stringList.Count != 0 && stringList.All(item => !string.IsNullOrWhiteSpace(item)))
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult(GetErrorMessage(validationContext));
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