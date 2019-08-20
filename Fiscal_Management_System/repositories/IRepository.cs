using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Fiscal_Management_System.repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Edit(T old_entity, T new_entity);
        void Remove(T entity);
    }
}
