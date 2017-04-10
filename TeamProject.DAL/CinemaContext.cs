using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL.Entities;

namespace TeamProject.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class CinemaContext : DbContext
    {
        public CinemaContext() : base("CinemaContext")
        {
            Database.CreateIfNotExists();
        }
        static CinemaContext() {
        //    Database.SetInitializer(
        //new DropCreateDatabaseIfModelChanges<CinemaContext>());
            //  Database.SetInitializer<CinemaContext>(new Configuration());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<View> Views { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<View>()
                    .HasRequired<User>(view => view.User) 
                    .WithMany(user => user.Views); 

            modelBuilder.Entity<View>()
                    .HasRequired<Movie>(view => view.Movie) 
                    .WithMany(movie => movie.Views); 
        }
    }

    
}
