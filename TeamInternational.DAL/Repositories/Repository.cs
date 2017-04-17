using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamInternational.DAL;
using TeamInternational.DAL.Entities.Interfaces;
using TeamInternational.DAL.Repositories.Interfaces;

namespace TeamInternational.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        CinemaContext db;
        DbSet<T> entities;

        public Repository(CinemaContext db)
        {
            this.db = db;
            entities = db.Set<T>();
        }

        public T Create(T item)
        {
            item = entities.Add(item);
            db.SaveChanges();
            return item;
        }

        public T Get(int id)
        {
            return entities.Find(id);
        }

        public void Remove(T item)
        {
            entities.Remove(item);
            db.SaveChanges();
        }

        public IEnumerable<T> Take(int count)
        {
            return entities.AsNoTracking().Take(count).ToList();
        }

        public List<T> ToList()
        {
            return entities.AsNoTracking().ToList();
        }

        public void Update(T item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            return entities.AsNoTracking().Where(predicate).ToList();
        }
    }
}
