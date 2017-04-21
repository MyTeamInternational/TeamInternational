using BLL.Abstract;
using BLL.ViewModels.Account;
using MvcUi.Infrastructure;
using Ninject;
using System.Web.Mvc;
using System.Web.Security;
using TeamProject.DAL.Entities;
using System.Linq;
using CONSTANTS;
using MvcUi.Infrastructure.Auth;

namespace MvcUi.Controllers
{
    [CustomErrorHandler]
    public class AccountController : Controller, IUrlFlow
    {
        [Inject]
        private IAccountManager accountManager;
        [Inject]
        public IAuthentication Auth { get; set; }
        [Inject]
        private IHomeUrlFlow urlFlow;
        public IHomeUrlFlow FLow { get { return urlFlow; } }
        public User CurrentUser
        {
            get
            {
                return ((IUserProvider)Auth.CurrentUser.Identity).User;
            }
        }
        public AccountController(IAccountManager accountManager, IAuthentication Auth,IHomeUrlFlow flow)
        {
            this.accountManager = accountManager;
            this.Auth = Auth;
            this.urlFlow = flow;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Auth.Login(model.Name, model.Password, false);
                //User user = accountManager.GetUser(model.Name);
                if (user != null)
                {
                    if (accountManager.CheckUserPassword(user, model.Password))
                    {
                        if (user.ConfirmedEmail)
                        {
                            //FormsAuthentication.SetAuthCookie(model.Name, true);
                            return RedirectToAction(Constans_Cinema.HOME_PAGE2, Constans_Cinema.HOME_CONTROLLER);//TODO вопрос а что если нужно эти названия хранить в отдельном класе перечисления?
                        }
                        else
                            ModelState.AddModelError("", "Не подтвержден Email");
                    }
                    else
                    {
                        ModelState["Password"].Errors.Add("Не верен пароль");
                    }
                }
                else
                {
                    ModelState["Name"].Errors.Add("Пользователя с таким логином нет");// как это переделывать под мультиязичный сайт
                }
            }
            return View("/Views/" + Constans_Cinema.HOME_CONTROLLER + "/" + Constans_Cinema.HOME_INDEX + ".cshtml", new Page1Model { LoginUser = model });
        }
        [UrlAction]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = accountManager.GetUser(model.Email);
                if (user == null)
                {
                    user = accountManager.CreateUser(model.Email, model.Password);
                    if (user != null)
                    {
                        //передаю в модуль отправления писем сгенерированную ссылку
                        string s = string.Format("Для завершения регистрации перейдите по ссылке:" +
                                                      "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                                          Url.Action("ConfirmEmail", "Account", new { Token = user.ID, Email = user.Email }, Request.Url.Scheme));
                        accountManager.SendEmailToUser(user, s);
                        return RedirectToAction(Constans_Cinema.ACCOUNT_CONFIRM, Constans_Cinema.ACCOUNT_CONTROLLER, new { Email = user.Email });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не удалось создать нового пользователя");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        public ViewResult Confirm(string Email)
        {
            TempData["messageEmail"] = "На почтовый адрес " + Email + " Вам высланы дальнейшие" +
                    "инструкции по завершению регистрации";
            return View();
        }
        [UrlAction]
        public ActionResult LogOut()
        {
            Auth.LogOut();
            return RedirectToAction(Constans_Cinema.HOME_INDEX, Constans_Cinema.HOME_CONTROLLER);
        }
        [UrlAction]
        public PartialViewResult UserName()
        {
            if (User.Identity.IsAuthenticated)
            {
                return PartialView(User);
            }
            return PartialView();
        }
        public ActionResult ConfirmEmail(string Token, string Email)
        {
            // можно ли при подтверждении мыла не шифровать токены если надо то каким шифром RCA, цезаря
            User user = accountManager.GetUser(int.Parse(Token));
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.ConfirmedEmail = true;
                    accountManager.UpdateUser(user);
                    Auth.Login(user.Name);
                    FormsAuthentication.SetAuthCookie(user.Name, true);
                    TempData["messageEmail"] = "Успешно подтвержден имейл";
                    FLow.StatusFlow = MyStatusFlow.Registred;
                    return View(Constans_Cinema.ACCOUNT_CONFIRM);
                }
                else
                {
                    return RedirectToAction(Constans_Cinema.ACCOUNT_CONFIRM, Constans_Cinema.ACCOUNT_CONTROLLER, new { Email = user.Email });
                }
            }
            else
            {
                return RedirectToAction(Constans_Cinema.ACCOUNT_CONFIRM, Constans_Cinema.ACCOUNT_CONTROLLER, new { Email = "" });
            }
        }
        public bool CanGo(string action)
        {
            return FLow.CanGo(action);
        }
        public ActionResult GetRedirect()
        {
            return new RedirectResult(FLow.GetRedirect());
        }
    }
}