using TeamInternational.DAL.Entities;
using TeamInternational.DAL.Repositories;
using TeamInternational.DAL.Repositories.Interfaces;

namespace TeamInternational.DAL.Interfaces
{
    public interface ICinemaWork
    {
        IRepository<User> Users { get; }
        IRepository<Movie> Movies { get; }
        IRepository<Picture> Pictures { get; }
    }

}