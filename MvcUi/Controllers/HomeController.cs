using BLL.Abstract;
using BLL.ViewModels.Account;
using MvcUi.Infrastructure;
using Ninject;
using System.Web.Mvc;
using CONSTANTS;

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
    [CustomErrorHandler]
    public class HomeController : Controller, IUrlFlow
    {
        [Inject]
        private static IHomeUrlFlow urlFlow;

        public static IHomeUrlFlow FLow { get { return HomeController.urlFlow; } }

        public HomeController(IHomeUrlFlow flow)
        {
            urlFlow = flow;
        }
        public bool CanGo(string action)
        {
            bool go = User.Identity.IsAuthenticated;
            if (action == Constans_Cinema.ACCOUNT_LOGOUT)
            {
                go = !User.Identity.IsAuthenticated;
            }
            HomeController.FLow.StatusFlow = (go) ? MyStatusFlow.Registred : MyStatusFlow.Not_Registred;

            return HomeController.FLow.CanGo(action);
        }

        public ActionResult GetRedirect()
        {
             return new RedirectResult(urlFlow.GetRedirect());
        }

        [UrlAction]
        public ActionResult Page1()
        {
            return View(new Page1Model { LoginUser = new LoginModel { } });
        }
        public ActionResult Page2()
        {
            return RedirectToAction(Constans_Cinema.MOVIE_INDEX, Constans_Cinema.MOVIE_CONTROLLER);
        }

    }


}