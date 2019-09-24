using System;
using System.Windows.Controls;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.service;

namespace Fiscal_Management_System.viewmodels.typeofservice
{
    public class TypeOfServiceAddViewModel : AddEditOperationViewModel<TypeOfService>
    {
        public override void OperateOnDatabase(TypeOfService entity)
        {
            Context.Set<TypeOfService>().Add(entity);
        }

        public TypeOfServiceAddViewModel()
        {
            Entity = new TypeOfService();
        }

        public TypeOfServiceAddViewModel(IDbContext context) : this()
        {
            Context = context;
        }
    }
}
