using BLL.Abstract;
using CONSTANTS;
using MvcUi.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUi.Controllers
{
    public class LastPageController : Controller, IUrlFlow
    {
        [Inject]
        private IHomeUrlFlow urlFlow;
        public IHomeUrlFlow FLow { get { return urlFlow; } }
        public LastPageController(IHomeUrlFlow flow)
        {
            this.urlFlow = flow;
        }
        public bool CanGo(string action)
            => FLow.CanGo(action);


        public ActionResult GetRedirect()
            => new RedirectResult(FLow.GetRedirect());

        // GET: LastPage
        [UrlAction]
        public ActionResult Page4()
        {
            return View();
        }
    }
}