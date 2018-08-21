using ASPNetCore.Localization.Extensions;
using ASPNetCore.Localization.Services;
using ASPNetCore.Localization.Services.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNetCore.Localization
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public static IConfiguration LocalConfiguration { get; set; }



        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LocalConfiguration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                //options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None;

            });

            services.Configure<IConfiguration>(Configuration);


            //services.AddLocalization(options =>
            //{
            //    options.ResourcesPath = "Resources";

            //});
            services.AddCustomLocalication();

            services.AddMvc(o =>
            {
                o.ModelMetadataDetailsProviders.Add(new CustomValidationMetaDataProvider());

            }).AddCustomMvcLocalization().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddScoped<IEmailService, EmailService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            // custom midlleware for detecting ui-culture in querystring
            //app.AddRequestLocalization();

            
            app.UseCustomLocalization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "LocalizedDefault",
                    template: "{ui-culture}/{controller=Home}/{action=Index}/{id?}"
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
