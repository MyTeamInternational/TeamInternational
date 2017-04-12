using BLL.Abstract;
using BLL.ViewModels.Movie;

using MvcUi.Infrastructure;
using Ninject;
using System.Collections.Generic;
using System.Web.Mvc;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;
using TeamProject.DAL;
using CONSTANTS;

namespace MvcUi.Controllers
{
    public class MovieController : Controller, IUrlFlow
    {
        [Inject]
        IMovieManager manager;
        [Inject]
        IMovieVMBuilder builder;

        CinemaContext db = new CinemaContext();

        public MovieController(IMovieManager manager, IMovieVMBuilder builder)
        {
            this.manager = manager;
            this.builder = builder;
        }
        // GET: Movie
        [UrlAction]
        public ActionResult Page2(string name = "")
        {

            return View((object)name);
            //returns the 3rd page Movie, main page for Movie redacting
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
            return HomeController.FLow.CanGo(action, MyStatusFlow.Registred.ParseUserAuth(User.Identity.IsAuthenticated));
        }

        public ActionResult GetRedirect()
        {
            return new RedirectResult(HomeController.FLow.GetRedirect());
        }
    }
}
