using MvcUi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                defaults: new { controller = CONSTANTS.HOME_CONTROLLER, action = CONSTANTS.HOME_INDEX, link = UrlParameter.Optional }
            );
        }
    }
}
