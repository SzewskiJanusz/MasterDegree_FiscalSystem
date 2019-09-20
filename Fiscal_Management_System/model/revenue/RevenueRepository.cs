using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.model.client
{
    public class RevenueRepository : EntityRepository<Revenue>, IDisposable
    {
        public RevenueRepository(FiscalDbContext context) : base(context)
        {
        }

        public List<string> GetAllNames()
        {
            return Context.Set<Revenue>().Select(x => x.Name).ToList();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
