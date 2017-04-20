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
using System.Linq;

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
        [Inject]
        private IHomeUrlFlow urlFlow;

        public IHomeUrlFlow FLow { get { return urlFlow; } }

        public MovieController(IMovieManager manager, IMovieVMBuilder builder,IHomeUrlFlow flow, IPictureManager pictureManager)
        {
            this.pictureManager = pictureManager;
            this.movieManager = manager;
            this.builder = builder;
            this.urlFlow = flow;
        }
        // GET: Movie
        [UrlAction]
        public ActionResult Page2(string name = "All")
        {
            return View((object)name);
         }

        public PartialViewResult Page2Data(string name = "All")
        {
           
                IEnumerable<Movie> resList = movieManager.GetMovies(5);
                if (name != "All")
                {
                    resList = movieManager.GetMovies(name);
                }
                var resModel = builder.GetVMList(resList);
                return PartialView(resModel);
                        
        }
        public ActionResult Autocomplete(string query = "")

        {

            if (Request.IsAjaxRequest())
            {
               

                var res = Json(movieManager.GetAutoCompliteFormat(query), JsonRequestBehavior.AllowGet);
                return   res;
            }else
            {
                return GetRedirect();
            }
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
            Movie m = movieManager.GetMovie(id);
            if (m != null)
            {
                return View(m);
            }
            else
            {
                return new RedirectResult(FLow.GetRedirect());
            }
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
            //кеш на уровень репозитория что
            //не на уровене контроллера
            //giud field for image -> wallpers
            //pagination ->masha 
            ////autocomptlite->pauluxxx
            //bool go = User.Identity.IsAuthenticated;
            //if (action == Constans_Cinema.ACCOUNT_LOGOUT)
            //{
            //    go = !User.Identity.IsAuthenticated;
            //}
            //HomeController.FLow.StatusFlow = (go) ? MyStatusFlow.Registred : MyStatusFlow.Not_Registred;

            return FLow.CanGo(action);
        }

        public ActionResult GetRedirect()
        {
            return new RedirectResult(FLow.GetRedirect());
        }
    }
}
