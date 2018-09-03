using ASPNETCore.ExceptionHandling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ASPNETCore.ExceptionHandling.Middlewares
{
    public class CustomExceptionHandlerMiddleware 
    {
        public readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public int HttpContextStatusCode { get; private set; }

        public async Task InvokeAsync(HttpContext context) {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
               await HandleException(context, ex);
            }

        }

        private Task HandleException(HttpContext context , Exception ex) {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            /// For API errors, responds with just the status code(no page).
            if (context.Features.Get<IHttpRequestFeature>().RawTarget.StartsWith("/api/", StringComparison.Ordinal))
            {
                return context.Response.WriteAsync(new ErrorDetail(ex)
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error"
                }.ToString());
            }

            context.Response.Redirect("/Home/Error/?errorId=" + Guid.NewGuid().ToString());
            return Task.FromResult(0);
        }

        private Task StatusCode(int statusCode)
        {
            throw new NotImplementedException();
        }
    }
}
