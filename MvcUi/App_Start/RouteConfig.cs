using CONSTANTS;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcUi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "",
                url: "{controller}/{action}/{link}",
                defaults: new { controller = Constans_Cinema.HOME_CONTROLLER, action = Constans_Cinema.HOME_INDEX, link = UrlParameter.Optional }
            );
        }
    }
}
