using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.model
{
    public interface IDbContext : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
