using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;
using TeamProject.DAL.Repositories.Interfaces;

namespace TeamProject.DAL
{

    public class UnitOfWork:ICinemaWork//to inject 
    {
        private CinemaContext db;

        private IRepository<User> userRepository;
        private IRepository<Movie> movieRepository;
        private IRepository<View> viewRepository;

        public UnitOfWork()
        {
            db = new CinemaContext();
        }

        public IRepository<User> Users
            => userRepository ?? (userRepository = new UserRepository(db));

        public IRepository<Movie> Movies
            => movieRepository ?? (movieRepository = new MovieRepository(db));

        public IRepository<View> Views
            => viewRepository ?? (viewRepository = new ViewRepository(db));

        public void Save()
            => db.SaveChanges();
    }
}
