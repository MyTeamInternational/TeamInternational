using BLL.Abstract;
using BLL.ViewModels.Account;
using MvcUi.Infrastructure;
using Ninject;
using System.Web.Mvc;
using System.Web.Security;
using TeamProject.DAL.Entities;

namespace MvcUi.Controllers
{
    [CustomErrorHandler]//:TODO куда его лучше положить?
    public class AccountController : Controller
    {
        [Inject]
        private IAccountManager accountManager;
        public AccountController(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = accountManager.GetUser(model.Name, model.Password);
                if (user != null)
                {
                    if (user.ConfirmedEmail)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        return RedirectToAction("Index", "Home");//TODO вопрос а что если нужно эти названия хранить в отдельном класе перечисления?
                    }
                    else
                        ModelState.AddModelError("", "Не подтвержден Email");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");// как это переделывать под мультиязичный сайт
                }
            }
            return View(model);
        }
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
                        string s = string.Format("Для завершения регистрации перейдите по ссылке:" +
                                                      "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                                          Url.Action("ConfirmEmail", "Account", new { Token = user.ID, Email = user.Email }, Request.Url.Scheme));
                        accountManager.SendEmailToUser(user, s);
                        return RedirectToAction("Confirm", "Account", new { Email = user.Email });
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
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Page1", "Home");
        }
        public ActionResult UserName() {
            if (User.Identity.IsAuthenticated)
            {
                return View(User);
            }
                return View();
        }

        public ActionResult ConfirmEmail(string Token, string Email)
        {
            User user = accountManager.GetUser(int.Parse(Token));
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.ConfirmedEmail = true;
                    accountManager.UpdateUser(user);

                    FormsAuthentication.SetAuthCookie(user.Name, true);
                    TempData["messageEmail"] = "Успешно подтвержден имейл";
                    return View("Confirm");
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Account", new { Email = "" });
            }
        }

    }
}