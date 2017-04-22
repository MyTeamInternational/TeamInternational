using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories.Interfaces;
using TeamProject.DAL;
using System.Web.Security;
using MvcUi.Controllers;
using CONSTANTS;
using BLL.Abstract;
using BLL.Helpers;

namespace MvcUi.Infrastructure.Auth
{
    public class CustomAuthentication : IAuthentication
    {
        [Inject]
        public IHomeUrlFlow urlFlow;
        public IHomeUrlFlow FLow { get { return urlFlow; } }
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private const string cookieName = "__AUTH_COOKIE";

        public HttpContext HttpContext { get; set; }

        [Inject]
        ICinemaWork work;

        public CustomAuthentication(ICinemaWork work,IHomeUrlFlow flow) { this.work = work;
            this.urlFlow = flow; }
        #region IAuthentication Members

        public User Login(string userName, string Password, bool isPersistent)
        {
            User retUser = work.Users.GetByEmailAndPassword(userName,Password);
            if (retUser != null&&retUser.ConfirmedEmail)
            {
                CreateCookie(userName, isPersistent);
            }
            HttpCookie authCookies = HttpContext.Response.Cookies.Get(cookieName);
            if (authCookies != null && !string.IsNullOrEmpty(authCookies.Value))

            {
                var ticket = FormsAuthentication.Decrypt(authCookies.Value);
                _currentUser = new UserProvider(ticket.Name, work.Users);
            }
            return retUser;
        }

        public User Login(string userName)
        {
            User retUser = work.Users.Items.FirstOrDefault(p => string.Compare(p.Email, userName, true) == 0);
            if (retUser != null)
            {
                CreateCookie(userName);
            }
            return retUser;
        }

        private void CreateCookie(string userName, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  userName,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var AuthCookie = new HttpCookie(cookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            HttpContext.Response.Cookies.Set(AuthCookie);
            if (CurrentUser.Identity.IsAuthenticated)
            {

                FLow.StatusFlow = MyStatusFlow.Registred;

            }
            FLow.StatusFlow = MyStatusFlow.Registred;

        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies[cookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
                FLow.StatusFlow = MyStatusFlow.Not_Registred;
            }

        }
        private IPrincipal _currentUser;

        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Request.Cookies.Get(cookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            _currentUser = new UserProvider(ticket.Name, work.Users);
                            urlFlow.StatusFlow = MyStatusFlow.Registred;
                        }
                        else
                        {
                            _currentUser = new UserProvider(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Failed authentication: " + ex.Message);
                        _currentUser = new UserProvider(null, null);
                    }
                }
                return _currentUser;
            }
        }

        #endregion
    }
}