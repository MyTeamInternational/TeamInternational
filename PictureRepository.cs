using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories.Interfaces;

namespace TeamProject.DAL.Repositories
{
    public class PictureRepository : IRepository<Picture>
    {
        private CinemaContext db;

        public IQueryable<Picture> Items 
            =>  db.Pictures; 

        // Maybe we should create new CinemaContext();
        public PictureRepository(CinemaContext db)
        {
            this.db = db;
        }

        public Picture Create(Picture picture)
            => db.Pictures.Add(picture);           
        

        public void Delete(Picture picture)
            => db.Pictures.Remove(picture);

        public IEnumerable<Picture> GetAll()
            => db.Pictures.ToList();

        public Picture Get(int id)
            => db.Pictures.SingleOrDefault(view => view.ID == id);

        public void Update(Picture picture)
            => db.Set<Picture>().AddOrUpdate(picture);
    }
}
