using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;

namespace ASPNetCore.Localization.Localization.Localizer
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        string _resourcesRelativePath;
        string _typeRelativeNamespace;
        CultureInfo _uiCulture;
        JObject _resourceCache;
        public JsonStringLocalizer(string resourcesRelativePath,
            string typeRelativeNamespace,
            CultureInfo uiCulture)
        {
            _resourcesRelativePath = resourcesRelativePath;
            _typeRelativeNamespace = typeRelativeNamespace;
            _uiCulture = uiCulture;
        }

        protected JObject GetResources()
        {
            if (_resourceCache == null)
            {
                string tag = _uiCulture.Name;

                string typeRelativePath =
                    _typeRelativeNamespace.Replace(".", "/");

                string filePath =
                    $"{_resourcesRelativePath}{typeRelativePath}/{tag}.json";

                string json = File.Exists(filePath) ?
                    File.ReadAllText(filePath, Encoding.Unicode) :
                    "{}";

                _resourceCache = JObject.Parse(json);
            }

            return _resourceCache;
        }
        public LocalizedString this[string name]
        {
            get
            {
                return this[name, null];
            }
        }

        public LocalizedString this[string name,params object[] arguments]
        {
            
            get
            {
                JObject resources = GetResources();

                string value = resources.Value<string>(name);

                bool resourceNotFound =
                    string.IsNullOrWhiteSpace(value);

                if (resourceNotFound)
                {
                    value = name;
                }
                else
                {
                    if (arguments != null)
                    {
                        value = string.Format(value, arguments);
                    }
                }

                return new LocalizedString(name,
                    value,
                    resourceNotFound);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            JObject resources = GetResources();
            foreach (var pair in resources)
            {
                yield return new LocalizedString(pair.Key,
                    pair.Value.Value<string>());
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new JsonStringLocalizer(_resourcesRelativePath,
                _typeRelativeNamespace,
                culture);
        }
    }

    public class JsonStringLocalizer<T> : JsonStringLocalizer, IStringLocalizer<T>
    {
        public JsonStringLocalizer(string resourcesRelativePath,
            string typeRelativeNamespace,
            CultureInfo uiCulture) : base(resourcesRelativePath , typeRelativeNamespace , uiCulture)
        {
           
        }
    }
}
