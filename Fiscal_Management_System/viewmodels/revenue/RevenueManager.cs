using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.revenue;
using System.Linq;

namespace Fiscal_Management_System.viewmodels.client
{
    public class RevenueManager : ComboBoxManager<Revenue>
    {
        public RevenueManager()
        {
            using (var ctx = new FiscalDbContext())
            {
                AllData = ctx.Set<Revenue>().ToList();
            }
        }
    }
}
