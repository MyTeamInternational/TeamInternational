using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories.Interfaces;

namespace TeamProject.DAL.Repositories
{
    public class MovieRepository : IRepository<Movie>
    {
        private CinemaContext db;
        public IQueryable<Movie> Items
        {
            get
            { return db.Movies; }
        }

        // Maybe we should create new CinemaContext();
        public MovieRepository(CinemaContext db)
        {
            this.db = db;
        }

        public void Create(Movie movie)
             => db.Movies.Add(movie);

        public void Delete(Movie movie)
            => db.Movies.Remove(movie);

        public IEnumerable<Movie> GetAll()
            => db.Movies.ToList();

        public Movie Get(int id)
            => db.Movies.SingleOrDefault(movie => movie.ID == id);

        public void Update(Movie movie)
            => db.Entry<Movie>(movie).State = EntityState.Modified;
    }
}
