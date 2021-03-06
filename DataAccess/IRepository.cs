﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetOverview(Func<T, bool> predicate = null);
        T GetDetails(Func<T, bool> predicate);
        void Add(T entity);
        void AddMany(IEnumerable<T> entities);
        void Delete(T entity);
    }
}
