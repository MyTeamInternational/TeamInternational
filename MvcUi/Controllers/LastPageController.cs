using CONSTANTS;
using MvcUi.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUi.Controllers
{
    public class LastPageController : Controller, IUrlFlow
    {
        public bool CanGo(string action)
            => HomeController.FLow.CanGo(action);


        public ActionResult GetRedirect()
            => new RedirectResult(HomeController.FLow.GetRedirect());

        // GET: LastPage
        [UrlAction]
        public ActionResult Page4()
        {
            return View();
        }
    }
}