using System;
using System.Web.Mvc;

namespace MvcUi.Infrastructure
{
    internal class CustomErrorHandlerAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
            {
            filterContext.ExceptionHandled = true;

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.Status = "500 Internal Server Error";
                filterContext.Result = new JsonResult { Data = new { ErrorMessage = filterContext.Exception.Message } };

            }
            else
            {
                string actionName = filterContext.RouteData.Values["action"].ToString();
                string controllerName = filterContext.RouteData.Values["controller"].ToString();
                var model = new System.Web.Mvc.HandleErrorInfo(filterContext.Exception, controllerName, actionName);
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(model)
                };
            }
            
        }
    }
}