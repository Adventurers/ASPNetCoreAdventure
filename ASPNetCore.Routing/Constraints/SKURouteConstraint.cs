using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace ASPNetCore.Routing.Constraints
{
    public class SKURouteConstraint : IRouteConstraint
    {
        public SKURouteConstraint()
        {

        }
        public bool Match(HttpContext httpContext, IRouter route, string routeKey,
            RouteValueDictionary values, RouteDirection routeDirection)
        {
            return ValidateToken(values["token"].ToString());
        }

        private bool ValidateToken(string token) {
            return false;
        }
    }
}
