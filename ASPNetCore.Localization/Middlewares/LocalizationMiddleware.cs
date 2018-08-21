using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore.Localization.Middlewares
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Query.ContainsKey("ui-culture"))
            {

                var tag = context.Request.Query["ui-culture"];

                CultureInfo.CurrentUICulture = new CultureInfo(tag);
            }
            await _next(context);
        }


    }
}
