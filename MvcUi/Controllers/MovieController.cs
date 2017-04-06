using BLL.Abstract;
using BLL.ViewModels.Movie;
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
        public ActionResult Index()
        {
            return View(List5());
        }
        //post должно ли это делаться асинхронно через аjax
        [HttpPost]
        public ActionResult Index(string name) {
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
        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
