using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fiscal_Management_System_UnitTests
{
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;
        private readonly IEnumerable<PropertyInfo> keys;
        public FakeDbSet()
        {
            keys = typeof(T).GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(KeyAttribute))
                            || "Id".Equals(p.Name, StringComparison.Ordinal) || 
                            "ID".Equals(p.Name, StringComparison.Ordinal))
                .ToList();
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable(); 
        }

        public virtual T Find(params object[] keyValues)
        {
            if (keyValues == null)
                throw new ArgumentNullException("keyValues");
            if (keyValues.Any(k => k == null))
                throw new ArgumentOutOfRangeException("keyValues");
            if (keyValues.Length != keys.Count())
                throw new ArgumentOutOfRangeException("keyValues");

            return _data.SingleOrDefault(i =>
                keys.Zip(keyValues, (k, v) => v.Equals(k.GetValue(i)))
                    .All(r => r)
            );
        }

        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local
        {
            get { return _data; }
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
