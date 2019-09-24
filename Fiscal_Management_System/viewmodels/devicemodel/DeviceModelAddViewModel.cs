using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.devicemodel;

namespace Fiscal_Management_System.viewmodels.devicemodel
{
    public class DeviceModelAddViewModel : AddEditOperationViewModel<DeviceModel>
    {
        public override void OperateOnDatabase(DeviceModel entity)
        {
            Context.Set<DeviceModel>().Add(entity);
        }

        public DeviceModelAddViewModel()
        {
            Entity = new DeviceModel();
        }

        public DeviceModelAddViewModel(IDbContext context) : this()
        {
            Context = context;
        }
    }
}
