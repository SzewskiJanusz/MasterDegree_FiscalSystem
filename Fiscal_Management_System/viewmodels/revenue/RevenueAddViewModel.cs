using System;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.revenue;

namespace Fiscal_Management_System.viewmodels.revenue
{
    public class RevenueAddViewModel : AddEditOperationViewModel<Revenue>
    {
        public override void OperateOnDatabase(Revenue entity)
        {
            Context.Set<Revenue>().Add(entity);
        }

        public RevenueAddViewModel()
        {
            Entity = new Revenue();
        }

        public RevenueAddViewModel(IDbContext context) : this()
        {
            Context = context;
        }
    }
}
