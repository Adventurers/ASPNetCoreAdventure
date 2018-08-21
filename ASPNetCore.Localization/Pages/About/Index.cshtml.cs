using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace ASPNetCore.Localization.Pages.About
{

    public class IndexModel : PageModel
    {
        IOptions<RequestLocalizationOptions> LocOptions;


        public IEnumerable<SelectListItem> supportedUICultures { get; set; }

        public IRequestCultureFeature cultureFeature { get; set; }
        public IndexModel(IOptions<RequestLocalizationOptions> locOptions)
        {
            LocOptions = locOptions;
        }
        public void OnGet()
        {
                supportedUICultures = LocOptions.Value
               .SupportedUICultures
               .Select(c => new SelectListItem
               {
                   Value = c.Name,
                   Text = c.DisplayName
               }).ToList();

            cultureFeature = PageContext.HttpContext.Features
                .Get<IRequestCultureFeature>();
        }

        public void OnPostSet(string uiCulture, string returnUrl)
        {
            IRequestCultureFeature feature =
               HttpContext.Features.Get<IRequestCultureFeature>();

            RequestCulture requestCulture =
                new RequestCulture(feature.RequestCulture.Culture,
                                   new CultureInfo(uiCulture));

            string cookieValue =
                CookieRequestCultureProvider.MakeCookieValue(requestCulture);

            string cookieName =
                CookieRequestCultureProvider.DefaultCookieName;

            //Response.Cookies.Append(cookieName, cookieValue , new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddHours(1) });
            PageContext.HttpContext.Response.Cookies.Append(cookieName, cookieValue);
            //return Page();
            //return RedirectToPage(returnUrl);
            //return LocalRedirect(returnUrl);
        }
    }
}