using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore.Routing.Constraints
{
    public class VersionRouteConstraint : IRouteConstraint
    {
        private int RequiredVersion { get; set; }

        public VersionRouteConstraint(int requireVersion)
        {
            RequiredVersion = requireVersion;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, 
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            var urlVersion =   values["version"].ToString()?.Substring(1);

            if (double.TryParse(urlVersion , out double requestVersion))
            {
                return requestVersion >= RequiredVersion && requestVersion < RequiredVersion + 1;
            }

            return false;
        }
    }
}
