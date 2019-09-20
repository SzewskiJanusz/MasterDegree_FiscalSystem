using Fiscal_Management_System.model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.repositories
{
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        protected IDbContext Context;
        protected string IncludePath { get; set; }

        public EntityRepository(IDbContext context)
        {
            Context = context;
        }

        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual IEnumerable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public virtual T GetByName(string name)
        {
            return Context.Set<T>().Find(name);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return IncludePath == null ? Context.Set<T>().ToList() : Context.Set<T>().Include(IncludePath).ToList();
        }

        public virtual void Edit(T old_entity, T new_entity)
        {
            Context.Entry(old_entity).CurrentValues.SetValues(new_entity);
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }
    }
}
