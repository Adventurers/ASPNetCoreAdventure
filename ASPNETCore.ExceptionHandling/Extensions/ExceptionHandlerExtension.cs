using ASPNETCore.ExceptionHandling.Middlewares;
using ASPNETCore.ExceptionHandling.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ASPNETCore.ExceptionHandling.Extensions
{
    public static class ExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder appBuilder)
        {

            appBuilder.UseExceptionHandler(app =>
            {
                app.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {

                        await context.Response.WriteAsync(new ErrorDetail(contextFeature.Error)
                        {
                            Message = "Internal Server Error",
                            StatusCode = context.Response.StatusCode
                        }.ToString());
                    }

                });
            });
        }

        public static void ConfigureExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
