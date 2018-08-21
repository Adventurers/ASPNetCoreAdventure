using ASPNetCore.Localization.Localization;
using ASPNetCore.Localization.Localization.Factories;
using ASPNetCore.Localization.Localization.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ASPNetCore.Localization.Extensions
{
    public static class LocalizationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder app , bool useDefault = false)
        {
            if (useDefault)
            {
                UseDefaultLocalizationConfig(app);
            }

            app.UseRequestLocalization();

            return app;
        }

        public static IServiceCollection AddCustomLocalication(this IServiceCollection services)
        {
            AddLocalicationFactory(services);
            AddJsonLocalizationOptions(services);
            AddRequestLocalizationOptions(services);
            return services;
        }
        public static IMvcBuilder AddCustomMvcLocalization(this IMvcBuilder builder)
        {
            AddMvcViewLocalization(builder);
            AddMvcDataAnnotationLocalization(builder);
            AddMvcRazorPagesLocalization(builder);
            return builder;
        }


        static void AddMvcViewLocalization(IMvcBuilder builder) {
            builder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
        }
        static void AddMvcDataAnnotationLocalization(IMvcBuilder builder)
        {
            builder.AddDataAnnotationsLocalization(options =>
             {
                 options.DataAnnotationLocalizerProvider = (type, factory) =>
                 {

                     var f = factory.Create(typeof(ErrorMessages));
                     return f;

                 };
             });
        }
        static void AddMvcRazorPagesLocalization(IMvcBuilder builder)
        {
            builder.AddRazorPagesOptions(options =>
            {
                options.Conventions.AddFolderRouteModelConvention("/", model =>
                {
                    foreach (var selector in model.Selectors)
                    {
                        var attributeRouteModel = selector.AttributeRouteModel;
                        attributeRouteModel.Template = AttributeRouteModel.CombineTemplates(attributeRouteModel.Template, "{ui-culture?}");

                    }
                });
            });
        }
        static void AddLocalicationFactory(IServiceCollection services)
        {

            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        }
        static void AddJsonLocalizationOptions(IServiceCollection services)
        {
            services.Configure<JsonLocalizationOptions>(options =>
            {
                options.ResourcesPath = "JsonResources";
            });
        }
        static void AddRequestLocalizationOptions(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>((options) =>
            {
                var langueges = new List<CultureInfo> {
                    new CultureInfo("fa-Ir"),
                    new CultureInfo("en-US")
                };
                options.SupportedCultures = langueges;
                options.SupportedUICultures = langueges;
                options.DefaultRequestCulture = new RequestCulture("fa-Ir");

                options.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());
                options.RequestCultureProviders.Insert(1, new CookieRequestCultureProvider());
                //options.RequestCultureProviders.Insert(1, new QueryRequestCultureProvider());
                options.RequestCultureProviders.Insert(2, new JsonRequestCultureProvider());

            });
        }

        static void UseDefaultLocalizationConfig(this IApplicationBuilder app) {

            var env = app.ApplicationServices.GetService<IHostingEnvironment>();

            var config = Startup.LocalConfiguration;
            var defaultCulture = config["Localization:DefaultCulture"];
            var cultures = config.GetSection("Localization:SupportedCultures").AsEnumerable().Skip(1).ToList();
            var uiCultures = config.GetSection("Localization:SupportedUICultures").AsEnumerable().Skip(1).ToList();
            var defaultRequestCulture = new RequestCulture(defaultCulture);
            var supportedCultures = new List<CultureInfo>();
            var supportedUICultures = new List<CultureInfo>();
            if (cultures.Count > 0)
            {
                supportedCultures = cultures.Select(i => new CultureInfo(i.Value)).ToList();
            }

            if (uiCultures.Count > 0)
            {
                supportedUICultures = uiCultures.Select(i => new CultureInfo(i.Value)).ToList();
            }

            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            localizationOptions.DefaultRequestCulture = defaultRequestCulture;
            localizationOptions.SupportedCultures = supportedCultures;
            localizationOptions.SupportedUICultures = supportedUICultures;

            app.UseMiddleware<RequestLocalizationMiddleware>(Options.Create(localizationOptions));
        }
    }
}
