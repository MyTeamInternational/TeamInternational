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

        private UserRepository userRepository;
        private IRepository<Movie> movieRepository;
        private IRepository<Picture> pictureRepository;

        public UnitOfWork()
        {
            db = new CinemaContext();
        }

        public UserRepository Users
            => userRepository ?? (userRepository = new UserRepository(db));

        public IRepository<Movie> Movies
            => movieRepository ?? (movieRepository = new MovieRepository(db));

        public IRepository<Picture> Pictures
            => pictureRepository ?? (pictureRepository = new PictureRepository(db));

        public void Save()
            => db.SaveChanges();
    }
}
