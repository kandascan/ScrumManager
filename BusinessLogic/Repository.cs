using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BusinessLogic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ScrumManagerDbContext _db = null;
        private DbSet<T> _objectSet;
        public Repository(ScrumManagerDbContext db)
        {
            _db = db;
            _objectSet = db.Set<T>();
        }

        public IEnumerable<T> GetOverview(Func<T, bool> predicate = null)
        {
            if (predicate != null)
                return _objectSet.Where(predicate);
            return _objectSet;
        }

        public T GetDetails(Func<T, bool> predicate)
        {
            return _objectSet.FirstOrDefault(predicate);
        }

        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
        }
    }
}
