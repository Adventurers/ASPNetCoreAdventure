using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace ASPNetCore.Localization.Localization.Providers
{
    public class RouteRequestCultureProvider : RequestCultureProvider
    {
        
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var CurrentLanguage = httpContext.GetRouteValue("lang") as string;

            if (string.IsNullOrEmpty(CurrentLanguage) )
            {
                return Task.FromResult((ProviderCultureResult)null);
            }
            

            return Task.FromResult(new ProviderCultureResult(CurrentLanguage, CurrentLanguage));
        }

       
    }
}
