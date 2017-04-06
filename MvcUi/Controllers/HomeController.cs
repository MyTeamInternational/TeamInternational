using BLL.ViewModels.Account;
using MvcUi.Infrastructure;
using System.Web.Mvc;

//1)demo class diagramm 
//presentation pps video
//2)как приготовиьт вопросы 
//вопросы по кукам или тп 
//только концептульные вопросы
//лекция практика по вопросам 
//найти баланс из 
//не засорять проект кучей библиотек
//нужни два три компонента 
//только если не хочете клиент на Ангуляре
//если не пришло пильмо 
//await вроде норм
//но обычно в больших системах есть отдельные  почтовые серверы 
//если что то валится то письмо не дойдет
//повторная регистрация у нас асинхроннстоть тру кетч это хорошее и разумное
//соблазн перейти на новую версию 
namespace MvcUi.Controllers
{
    [CustomErrorHandler]//куда его лучше положить?
    // как релизовать постоянный редирект на Page1 при остальных страницах нужен свой фильтр?
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