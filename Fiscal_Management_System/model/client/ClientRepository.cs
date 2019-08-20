using Fiscal_Management_System.repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.model.client
{
    public class ClientRepository : EntityRepository<Client>, IDisposable
    {
        public ClientRepository(FiscalDbContext context) : base(context)
        {
            IncludePath = "Revenue";
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
