using ASPNetCore.Localization.Localization.Localizer;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;

namespace ASPNetCore.Localization.Localization.Factories
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ConcurrentDictionary<string, JsonStringLocalizer> _localizerCache =
          new ConcurrentDictionary<string, JsonStringLocalizer>();

        string _resourcesRelativePath;
        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> options)
        {
            _resourcesRelativePath =
                options.Value?.ResourcesPath ?? string.Empty;
        }
        public IStringLocalizer Create(Type resourceSource)
        {
            TypeInfo typeInfo =
                resourceSource.GetTypeInfo();

            AssemblyName assemblyName =
                new AssemblyName(typeInfo.Assembly.FullName);

            string baseNamespace =
                assemblyName.Name;

            string typeRelativeNamespace =
                typeInfo.FullName.Substring(baseNamespace.Length);

            return _localizerCache.GetOrAdd(typeRelativeNamespace + CultureInfo.CurrentUICulture.Name, new JsonStringLocalizer(_resourcesRelativePath,
                typeRelativeNamespace,
                CultureInfo.CurrentUICulture));
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            string typeRelativeNamespace = baseName.Substring(location.Length);

            return _localizerCache.GetOrAdd(typeRelativeNamespace + CultureInfo.CurrentUICulture.Name, 
                new JsonStringLocalizer(_resourcesRelativePath,
             typeRelativeNamespace,
             CultureInfo.CurrentUICulture));
        }
    }
}
