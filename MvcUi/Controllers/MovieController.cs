using BLL.Abstract;
using BLL.ViewModels.Movie;
using MvcUi.Infrastructure;
using Ninject;
using System.Collections.Generic;
using System.Web.Mvc;
using TeamProject.DAL.Entities;

namespace MvcUi.Controllers
{
    public class MovieController : Controller
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
         public ActionResult Page2()
        {
            return View(List5());
        }
        //post должно ли это делаться асинхронно через аjax
        [HttpPost]
        public ActionResult Page2(string name) {
            var resList = manager.GetMovies(name);
            var resModel = builder.GetVMList(resList);
            return View(resModel);
        }
        
        private IEnumerable<MovieModel>  List5()
        {
            IEnumerable<Movie> resultList = manager.GetMovies(5);
            IEnumerable<MovieModel> resultListModels = builder.GetVMList(resultList);
            return resultListModels;
        }
    }
}
