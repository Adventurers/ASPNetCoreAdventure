﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore.Localization.Localization.Providers
{
    public class QueryRequestCultureProvider : RequestCultureProvider
    {
        public static readonly string DefaultParamterName = "ui-culture";

        public string QureyParamterName { get; set; } = DefaultParamterName;

        /// <inheritdoc />
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var query = httpContext.Request.Query;
            var exists = query.TryGetValue("ui_locales", out StringValues culture);

            if (!exists)
            {
                exists = query.TryGetValue("returnUrl", out StringValues requesturl);

                if (exists)
                {
                    var request = requesturl.ToArray()[0];
                    Uri uri = new Uri("http://faketit.com" + request);
                    var query1 = QueryHelpers.ParseQuery(uri.Query);
                    var requestCulture = query1.FirstOrDefault(t => t.Key == "ui_locales").Value;

                    var cultureFromReturnUrl = requestCulture.ToString();
                    if (string.IsNullOrEmpty(cultureFromReturnUrl))
                    {
                        return NullProviderCultureResult;
                    }

                    culture = cultureFromReturnUrl;
                }
            }

            var providerResultCulture = ParseDefaultParamterValue(culture);

            // Use this cookie for following requests, so that for example the logout request will work
            if (!string.IsNullOrEmpty(culture.ToString()))
            {
                var cookie = httpContext.Request.Cookies[".AspNetCore.Culture"];
                var newCookieValue = Microsoft.AspNetCore.Localization.CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));

                if (string.IsNullOrEmpty(cookie) || cookie != newCookieValue)
                {
                    httpContext.Response.Cookies.Append(".AspNetCore.Culture", newCookieValue);
                }
            }

            return Task.FromResult(providerResultCulture);
        }

        public static ProviderCultureResult ParseDefaultParamterValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            var cultureName = value;
            var uiCultureName = value;

            if (cultureName == null && uiCultureName == null)
            {
               
                return null;
            }

            if (cultureName != null && uiCultureName == null)
            {
                uiCultureName = cultureName;
            }

            if (cultureName == null && uiCultureName != null)
            {
                cultureName = uiCultureName;
            }

            return new ProviderCultureResult(cultureName, uiCultureName);
        }
    }
}
