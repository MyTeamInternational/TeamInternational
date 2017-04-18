using BLL.Abstract;
using BLL.ViewModels.Movie;

using MvcUi.Infrastructure;
using Ninject;
using System.Collections.Generic;
using System.Web.Mvc;
using TeamProject.DAL.Entities;
using CONSTANTS;
using BLL.Helpers;
using System.Web;
using System.IO;

namespace MvcUi.Controllers
{
    public class MovieController : Controller, IUrlFlow
    {
        [Inject]
        IPictureManager pictureManager;
        [Inject]
        IMovieManager movieManager;
        [Inject]
        IMovieVMBuilder builder;

        public MovieController(IPictureManager pictureManager, IMovieManager movieManager, IMovieVMBuilder builder)
        {
            this.pictureManager = pictureManager;
            this.movieManager = movieManager;
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
            IEnumerable<Movie> resList = movieManager.GetMovies(5);
            if (name != "")
            {
                resList = movieManager.GetMovies(name);
            }
            var resModel = builder.GetVMList(resList);
            return PartialView(resModel);
        }

        [HttpPost]
        public ActionResult Create(MovieModel movieModel)
        {
            Movie movie = movieManager.CreateMovie(movieModel.GetByModel());
            return RedirectToRoute(Constans_Cinema.DEFAULT_ROUTE, new { action = Constans_Cinema.MOVIE_EDIT, controller = Constans_Cinema.MOVIE_CONTROLLER, id = movie.ID });
        }
        [HttpGet]
        [ActionName(Constans_Cinema.MOVIE_EDIT)]
        
        public ActionResult Page3(int id)
        {
            return View(movieManager.GetMovie(id));
        }
        [HttpPost]
        public ActionResult Update(Movie movie, List<HttpPostedFileBase> files)
        {
            pictureManager.CreatePictures(movie.ID, files);
            movieManager.Update(movie);
            return RedirectToAction(Constans_Cinema.LAST_PAGE_INDEX, Constans_Cinema.LAST_PAGE_CONTROLLER);
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
