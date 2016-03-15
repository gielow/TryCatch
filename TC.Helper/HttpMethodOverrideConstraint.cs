using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace TC.Helper
{
    public class HttpMethodOverrideConstraint : HttpMethodConstraint
    {
        public HttpMethodOverrideConstraint(params string[] allowedMethods) : base(allowedMethods)
        {
        }

        protected override bool Match(HttpContextBase httpContext, Route route,
            string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var methodOverride = httpContext.Request.Form["X-HTTP-Method-Override"];

            if (methodOverride == null)
                return base.Match(httpContext, route, parameterName,
                    values, routeDirection);

            return
                AllowedMethods.Any(m =>
                    string.Equals(m, httpContext.Request.HttpMethod,
                        StringComparison.OrdinalIgnoreCase))
                &&
                AllowedMethods.Any(m =>
                    string.Equals(m, methodOverride,
                        StringComparison.OrdinalIgnoreCase))
            ;
        }
    }
}
