using Microsoft.AspNetCore.Builder;

namespace ASPNetCore.Localization.Middlewares
{
    public static class LocalizationMiddlewareExtension
    {
        public static IApplicationBuilder AddRequestLocalization(this IApplicationBuilder app)
        {
           return app.UseMiddleware<LocalizationMiddleware>();
        }
    }
}
