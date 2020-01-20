using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.views.service;

namespace Fiscal_Management_System.viewmodels.service
{
    public class PlannedServicesViewModel : EntityViewModel<Service>
    {
        private Serviceman loggedUser;

        public PlannedServicesViewModel(Func<UserControl, int> ucSetMethod, Serviceman serviceman) : base(ucSetMethod)
        {
            loggedUser = serviceman;
            EntitySearcher.Collection = GetDataFromDB();
        }

        public PlannedServicesViewModel(Func<UserControl, int> ucSetMethod, IDbContext context) : base(ucSetMethod, context)
        {
            Context = context;
        }

        public ObservableCollection<Service> GetDataFromDB()
        {
            ObservableCollection<Service> plannedServices;
            using (var context = (Context == null ? new FiscalDbContext() : Context))
            {
                plannedServices = new ObservableCollection<Service>(
                    context.Set<Service>().
                        Include("Device").
                        Include("Device.Place").
                        Include("Device.Client").
                        Include("TypeOfService").
                        Where(x => !x.ExecutionTime.HasValue).
                        ToList());
            }

            return plannedServices;
        }

        /// <summary>
        /// Go to service's details
        /// </summary>
        private ICommand _goToServiceDetailsButtonCommand;
        public ICommand GoToServiceDetailsButtonCommand
        {
            get
            {
                _goToServiceDetailsButtonCommand = new RelayCommand(o =>
                {
                    Service serv = new Service((Service)o);
                    ServiceDetails detailsWindow = new ServiceDetails(serv);
                    detailsWindow.ShowDialog();

                }, o => true);

                return _goToServiceDetailsButtonCommand;
            }
        }

        /// <summary>
        /// Go to done services
        /// </summary>
        private ICommand _goToDoneServicesButtonCommand;
        public ICommand GoToDoneServicesButtonCommand
        {
            get
            {
                _goToDoneServicesButtonCommand = new RelayCommand(o =>
                {
                    DoneServices ds = new DoneServices(UserControlSwitcher, loggedUser);
                    UserControlSwitcher(ds);
                }, o => true);

                return _goToDoneServicesButtonCommand;
            }
        }
    }
}
