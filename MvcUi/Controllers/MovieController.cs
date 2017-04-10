using BLL.Abstract;
using BLL.ViewModels.Movie;
using Ninject;
using System.Collections.Generic;
using System.Web.Mvc;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;
using TeamProject.DAL;

namespace MvcUi.Controllers
{
    public class MovieController : Controller
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
        public ActionResult Index()
        {
            //return View();
            return View(List5());
            //returns the 3rd page Movie, main page for Movie redacting
        }
        //post
        [HttpPost]
        public ActionResult Index(string name) {
            var resList = manager.GetMovies(name);
            var resModel = builder.GetVMList(resList);
            return View(resModel);
        }
        
        private IEnumerable<MovieModel> List5()
        // private ActionResult List5()
        {
            IEnumerable<Movie> resultList = manager.GetMovies(5);
            IEnumerable<MovieModel> resultListModels = builder.GetVMList(resultList);
            return resultListModels;
            //return View(resultListModels);
        }
        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            try
            {
                // TODO: Add insert logic here
                db.Movies.Add(movie);
                db.SaveChanges();
                TempData["ID"] = movie.ID;
                return RedirectToAction("Edit");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Movie movie = db.Movies.Find(id);
            return View(movie);
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
