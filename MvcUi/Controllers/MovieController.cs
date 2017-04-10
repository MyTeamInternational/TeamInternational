using BLL.Abstract;
using BLL.ViewModels.Movie;
using MvcUi.Infrastructure;
using Ninject;
using System.Collections.Generic;
using System.Web.Mvc;
using TeamProject.DAL.Entities;
using System;

namespace MvcUi.Controllers
{
    public class MovieController : Controller, IUrlFlow
    {
        [Inject]
        IMovieManager manager;
        [Inject]
        IMovieVMBuilder builder;
        public MovieController(IMovieManager manager, IMovieVMBuilder builder)
        {
            this.manager = manager;
            this.builder = builder;
        }
        // GET: Movie
        public ActionResult Page2(string name = "")
        {
            return View((object)name);
        }
        public PartialViewResult Page2Data(string name = "")
        {
            IEnumerable<Movie> resList = manager.GetMovies(5);
            if (name != "")
            {
                resList = manager.GetMovies(name);
            }
            var resModel = builder.GetVMList(resList);
            return PartialView(resModel);
        }


        public bool CanGo(string action)
        {
            return HomeController.FLow.CanGo(action, User.Identity.IsAuthenticated);
        }

        public ActionResult GetRedirect()
        {
            return new RedirectResult(HomeController.FLow.GetRedirect());
        }
    }
}
