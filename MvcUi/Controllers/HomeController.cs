using BLL.Abstract;
using BLL.ViewModels.Account;
using MvcUi.Infrastructure;
using Ninject;
using System.Web.Mvc;
using System.Linq;

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
    // как релизовать постоянный редирект на Page1 при остальных страницах нужен свой фильтр?
    public class HomeController : Controller, IUrlFlow
    {
        [Inject]
        private IHomeUrlFlow urlFlow;

        public HomeController(IHomeUrlFlow flow)
        {
            flow.CanUseAction.Add(CONSTANTS.HOME_INDEX, true);
            flow.CanUseAction.Add(CONSTANTS.HOME_PAGE2, false);
            this.urlFlow = flow;
        }
        public bool CanGo(string action)
        {
            if (User.Identity.IsAuthenticated)
            {
                urlFlow.CanUseAction[CONSTANTS.HOME_INDEX] = false;
                urlFlow.CanUseAction[CONSTANTS.HOME_PAGE2] = true;

            }
            return urlFlow.CanUseAction[action];
        }

        public ActionResult GetRedirect()
        {
            string buffer = "/" + CONSTANTS.HOME_CONTROLLER + "/" + urlFlow.CanUseAction.FirstOrDefault((v) => v.Value == true).Key;
            RedirectResult redirect = new RedirectResult(buffer);
            return redirect;
        }

        [UrlAction]
        public ActionResult Page1()
        {
            return View(new Page1Model { LoginUser = new LoginModel { } });
        }
        [UrlAction]
        public ActionResult Page2()
        {
            return RedirectToAction(CONSTANTS.MOVIE_INDEX, CONSTANTS.MOVIE_CONTROLLER);
        }

    }


}