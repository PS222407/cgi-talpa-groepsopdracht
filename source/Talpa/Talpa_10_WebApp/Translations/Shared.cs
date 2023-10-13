using Microsoft.Extensions.Localization;

namespace Talpa_10_WebApp.Translations;

public class Shared
{
    private readonly IStringLocalizer<Shared> _localizer;

    public Shared(IStringLocalizer<Shared> localizer)
    {
        _localizer = localizer;
    }

    public string Get(string key)
    {
        return _localizer[key];
    }
}