using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.views.device;

namespace Fiscal_Management_System.viewmodels.service
{
    public class SetServiceAsDoneViewModel
    {
        public Service Service { get; set; }
        public IDbContext Context { get; set; }

        public SetServiceAsDoneViewModel(Service s)
        {
            this.Service = s;
        }

        public SetServiceAsDoneViewModel(Service s, IDbContext context)
        {
            this.Service = s;
            this.Context = context;
        }

        private ICommand _confirmButtonCommand;
        public ICommand ConfirmButtonCommand
        {
            get
            {
                _confirmButtonCommand = new RelayCommand(o =>
                {
                    AddDateOfExecutionInService((Service)o);
                    
                }, o => true);

                return _confirmButtonCommand;
            }
        }

        public void AddDateOfExecutionInService(Service service)
        {
            using (var ctx = Context == null ? new FiscalDbContext() : Context)
            {
                var entityInDb = ctx.Set<Service>().Find(service.Id);
                entityInDb.ExecutionTime = service.PlannedDateOfExecution;
                entityInDb.ChargedPrice = service.ChargedPrice;
                ctx.SaveChanges();
            }
        }
    }
}
