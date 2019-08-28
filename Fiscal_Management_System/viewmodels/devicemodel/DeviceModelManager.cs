using Fiscal_Management_System.model;
using Fiscal_Management_System.model.devicemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.viewmodels.devicemodel
{
    public class DeviceModelManager : ComboBoxManager<DeviceModel>
    {
        public DeviceModelManager()
        {
            using (var ctx = new FiscalDbContext())
            {
                AllData = ctx.DeviceModels.ToList();
            }
        }
    }
}
