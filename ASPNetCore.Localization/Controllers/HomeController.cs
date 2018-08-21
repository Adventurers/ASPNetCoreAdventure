using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNetCore.Localization.Models;
using ASPNetCore.Localization.Services;
using ASPNetCore.Localization.Localization;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Http;

namespace ASPNetCore.Localization.Controllers
{
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class HomeController : Controller
    {
        public IEmailService MailService { get; set; }

        public IStringLocalizer<Shared> _sharedLocalizer { get; set; }

        public HomeController(IEmailService mailService, IStringLocalizer<Shared> sharedLocalizer)
        {
            MailService = mailService;
            _sharedLocalizer = sharedLocalizer; ;
        }

        public async Task<IActionResult> Index()
        {
            var feature = HttpContext.Features.Get<IRequestCultureFeature>();

            if (feature != null)
            {
                var name = feature.RequestCulture.UICulture.Name;

                var requestCulture = new RequestCulture(feature.RequestCulture.Culture, new CultureInfo(name));

                var cookievalue = CookieRequestCultureProvider.MakeCookieValue(requestCulture);

                var cookieName = CookieRequestCultureProvider.DefaultCookieName;

                // Response.Cookies.Append(cookieName , cookievalue);
            }

            var result = await MailService.SendEmail("", "", "");

            var title = _sharedLocalizer["title"];

            return Content(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
