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
    public class ViewRepository : IRepository<View>
    {
        private CinemaContext db;

        public IQueryable<View> Items
        {
            get
            { return db.Views; }
        }

        // Maybe we should create new CinemaContext();
        public ViewRepository(CinemaContext db)
        {
            this.db = db;
        }

        public void Create(View view)
            => db.Views.Add(view);

        public void Delete(View view)
            => db.Views.Remove(view);

        public IEnumerable<View> GetAll()
            => db.Views.ToList();

        public View Get(int id)
            => db.Views.SingleOrDefault(view => view.ID == id);

        public void Update(View view)
            => db.Entry<View>(view).State = EntityState.Modified;
    }
}
