using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiscal_Management_System.model.service;

namespace Fiscal_Management_System.viewmodels.service
{
    public class SetServiceAsDoneViewModel
    {
        public Service Service { get; set; }

        public SetServiceAsDoneViewModel(Service s)
        {
            this.Service = s;
        }
    }
}
