using BLL.ViewModels.Account;
using MvcUi.Infrastructure;
using System.Web.Mvc;

namespace MvcUi.Controllers
{
    [CustomErrorHandler]//куда его лучше положить?
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Page1Model indexVM = new Page1Model { isAutorized=false,UserName=null};
            if (User.Identity.IsAuthenticated)
            {
                indexVM.UserName = User.Identity.Name;
                indexVM.isAutorized = true;
              //  return RedirectToAction("Movie","List");
            }
            return View(indexVM);
            
        }
        public ActionResult Page1() {
            Page1Model model = new Page1Model { isAutorized = false, UserName = null };
            if (User.Identity.IsAuthenticated)
            {
                model.UserName = User.Identity.Name;
                model.isAutorized = true;
                //  return RedirectToAction("Movie","List");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        [Authorize]
        public ActionResult Page2() {
            return RedirectToAction("Movie","List");
        }

    }

   
}