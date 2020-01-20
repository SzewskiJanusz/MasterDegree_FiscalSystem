using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
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
            Service s;
            Service entityInDb;
            DateTime next;
            Device d;
            using (var ctx = Context == null ? new FiscalDbContext() : Context)
            {
                entityInDb = ctx.Set<Service>().Include("Device").Include("Device.Client").Include("Device.Place").FirstOrDefault(x => x.Id == service.Id);
                entityInDb.ExecutionTime = service.PlannedDateOfExecution;
                entityInDb.ChargedPrice = service.ChargedPrice;
                next = entityInDb.ExecutionTime.Value.
                    AddMonths(entityInDb.Device.InspectionPeriodicTimeInMonths);
                d = new Device(entityInDb.Device);
                s = new Service(service);
                s.Device = ctx.Set<Device>().FirstOrDefault(x => x.UniqueNumber == d.UniqueNumber);
                s.TypeOfService = ctx.Set<TypeOfService>().FirstOrDefault(x => x.Name == "Przegląd kasy fiskalnej");
                s.ApproveTime = (DateTime)entityInDb.ExecutionTime;
                s.ExecutionTime = null;
                s.PlannedDateOfExecution = next;
                ctx.Set<Service>().Add(s);
                ctx.SaveChanges();
            }
        }
    }
}
