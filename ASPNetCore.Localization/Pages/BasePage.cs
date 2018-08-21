using ASPNetCore.Localization.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPNetCore.Localization.Pages
{
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class BasePage : PageModel
    {
    }
}
