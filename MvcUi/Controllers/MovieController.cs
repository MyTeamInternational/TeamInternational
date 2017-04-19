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
using System.Linq;

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
        public ActionResult Page2(string name = "All")
        {

            return View((object)name);
         }

        public PartialViewResult Page2Data(string name = "All")
        {
           
                IEnumerable<Movie> resList = manager.GetMovies(5);
                if (name != "All")
                {
                    resList = manager.GetMovies(name);
                }
                var resModel = builder.GetVMList(resList);
                return PartialView(resModel);
                        
        }
        public ActionResult Autocomplete(string query = "")

        {

            if (Request.IsAjaxRequest())
            {
               

                var res = Json(manager.GetAutoCompliteFormat(query), JsonRequestBehavior.AllowGet);
                return   res;
            }else
            {
                return GetRedirect();
            }
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
            Movie m = manager.GetMovie(id);
            if (m != null)
            {
                return View(m);
            }
            else
            {
                return new RedirectResult(HomeController.FLow.GetRedirect());
            }
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
            // manager.UploadImage(file);
            manager.Update(movie);
            HomeController.FLow.StatusFlow = MyStatusFlow.Smile;
            return RedirectToAction(Constans_Cinema.LAST_PAGE_INDEX, Constans_Cinema.LAST_PAGE_CONTROLLER);
        }

        public bool CanGo(string action)
        {
            //кеш на уровень репозитория что
            //не на уровене контроллера
            //giud field for image -> wallpers
            //pagination ->masha 
            //autocomptlite->pauluxxx
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
            return new RedirectResult(HomeController.FLow.GetRedirect());
        }
    }
}
