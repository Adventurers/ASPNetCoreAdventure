﻿using ASPNETCore.ExceptionHandling.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ASPNETCore.ExceptionHandling.Extensions
{

    public static class HttpStatusCodeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpStatusCodeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpStatusCodeExceptionMiddleware>();
        }
    }
}
