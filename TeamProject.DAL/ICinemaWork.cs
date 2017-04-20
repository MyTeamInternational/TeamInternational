﻿using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;
using TeamProject.DAL.Repositories.Interfaces;

namespace TeamProject.DAL
{
    public interface ICinemaWork
    {
        UserRepository Users { get; }
        IRepository<Movie> Movies { get; }
        IRepository<View> Views { get; }
        void Save();
    }

}