using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamInternational.DAL.Entities;
using TeamInternational.DAL.Interfaces;
using TeamInternational.DAL.Repositories;
using TeamInternational.DAL.Repositories.Interfaces;

namespace TeamInternational.DAL
{

    public class CinemaWork : ICinemaWork 
    {
        private CinemaContext db;

        private IRepository<User> userRepository;
        private IRepository<Movie> movieRepository;
        private IRepository<Picture> pictureRepository;

        public CinemaWork()
        {
            db = new CinemaContext();
        }

        public IRepository<User> Users
            => userRepository ?? (userRepository = new Repository<User>(db));

        public IRepository<Movie> Movies
            => movieRepository ?? (movieRepository = new Repository<Movie>(db));

        public IRepository<Picture> Pictures
            => pictureRepository ?? (pictureRepository = new Repository<Picture>(db));
    }
}
