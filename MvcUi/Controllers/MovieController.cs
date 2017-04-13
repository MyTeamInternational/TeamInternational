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
using BLL.Helpers;
using System.Web;
using System.IO;

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

        [HttpPost]
        public ActionResult Create(MovieModel movieModel)
        {
            Movie movie = manager.CreateMovie(movieModel.GetByModel());
            return RedirectToRoute(Constans_Cinema.DEFAULT_ROUTE, new { action = Constans_Cinema.MOVIE_EDIT, controller = Constans_Cinema.MOVIE_CONTROLLER, id = movie.ID });
        }
        [HttpGet]
        [ActionName(Constans_Cinema.MOVIE_EDIT)]
        
        public ActionResult Page3(int id)
        {
            return View(manager.GetMovie(id));
        }
        [HttpPost]
        public ActionResult Update(Movie movie, HttpPostedFileBase file)
        {
            var path = "";
            var relativePath = "";
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    if (Path.GetExtension(file.FileName).ToLower() == ".jpg"
                        || Path.GetExtension(file.FileName).ToLower() == ".png"
                        || Path.GetExtension(file.FileName).ToLower() == ".gif"
                        || Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                    {
                        path = Path.Combine(Server.MapPath("~/Content/Images"), file.FileName);
                        relativePath = "~/Content/Images/" + file.FileName;
                        file.SaveAs(path);
                        ViewBag.UploadSuccess = true;
                        
                    }
                }
            }

            movie.ImagePath = relativePath;
            manager.Update(movie);
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
