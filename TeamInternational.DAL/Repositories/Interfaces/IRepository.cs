using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamInternational.DAL.Entities.Interfaces;

namespace TeamInternational.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        T Create(T item);
        T Get(int id);
        IEnumerable<T> Take(int count);
        IEnumerable<T> Where(Func<T, bool> predicate);
        void Remove(T item);
        void Update(T item);
        List<T> ToList();
    }
}
