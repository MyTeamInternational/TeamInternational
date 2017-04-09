using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUi.Infrastructure
{
    public class UrlActionAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var urlValues = filterContext.RouteData.Values;
            var controller = filterContext.Controller as IUrlFlow;
            if (controller != null)
            {
                if (!controller.CanGo(urlValues["action"].ToString()))
                {
                    filterContext.Result = new RedirectResult("/"+CONSTANTS.HOME + "/"+CONSTANTS.HOME_INDEX);
                }
            }
        }
    }
}