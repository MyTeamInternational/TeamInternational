//using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamInternational.DAL.Entities;
using TeamInternational.DAL.Interfaces;

namespace TeamInternational.DAL
{
    public class CinemaContext : DbContext
    {
        public CinemaContext() : base("CinemaContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Picture>()
                    .HasRequired<Movie>(picture => picture.Movie) 
                    .WithMany(movie => movie.Pictures); 
        }
    }

    
}
