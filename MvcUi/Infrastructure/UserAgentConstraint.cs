using System.Web;
using System.Web.Routing;

namespace MvcUi.Infrastructure
{
    public class UserAgentConstraint : IRouteConstraint
    {
        private string requiredUserAgent;
        public UserAgentConstraint(string agentParam)
        {
            requiredUserAgent = agentParam;
        }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName,
        RouteValueDictionary values, RouteDirection routeDirection)
        {
            bool match = false;            
            var ss = values["controller"];
            match = httpContext.Request.UserAgent != null &&
            httpContext.Request.UserAgent.Contains(requiredUserAgent);

            return match;
        }
    }
}