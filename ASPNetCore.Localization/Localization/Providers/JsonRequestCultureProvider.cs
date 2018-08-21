using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Threading.Tasks;

namespace ASPNetCore.Localization.Localization.Providers
{
    public class JsonRequestCultureProvider : RequestCultureProvider
    {
        public static readonly string DefaultJsonFileName = "AppSettings.json";

        public static readonly string LocalizationSection = "Localization";

        public string JsonFileName { get; set; } = DefaultJsonFileName;

        public string CultureKey { get; set; } = "Culture";

        public string UICultureKey { get; set; } = "UICulture";

        //public IConfigurationRoot Configuration { get; set; }

     

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException();
            }

            //SetConfiguration(httpContext);

            string culture = null;
            string uiCulture = null;
            var localizationSection = Startup.LocalConfiguration.GetSection(LocalizationSection);

            if (!string.IsNullOrEmpty(CultureKey))
            {
                culture = localizationSection[CultureKey];
            }

            if (!string.IsNullOrEmpty(UICultureKey))
            {
                uiCulture = localizationSection[UICultureKey];
            }

            if (culture == null && uiCulture == null)
            {
                return Task.FromResult((ProviderCultureResult)null);
            }

            if (culture != null && uiCulture == null)
            {
                uiCulture = culture;
            }

            if (culture == null && uiCulture != null)
            {
                culture = uiCulture;
            }

            var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

            return Task.FromResult(providerResultCulture);
        }

        //private void SetConfiguration(HttpContext httpContext) {
        //    var env = httpContext.RequestServices.GetService<IHostingEnvironment>();
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile(JsonFileName);

        //    Configuration = builder.Build();
        //}
    }
}
