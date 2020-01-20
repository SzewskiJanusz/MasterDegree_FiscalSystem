using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.repairgroup;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.views.service;

namespace Fiscal_Management_System.viewmodels.service
{
    public class DoneServicesViewModel : EntityViewModel<Service>, INotifyPropertyChanged
    {
        private string _showServicesButtonName;
        public string ShowServicesButtonName
        { get { return _showServicesButtonName; } set { _showServicesButtonName = value; OnPropertyChanged("ShowServicesButtonName"); } }

        private Serviceman serviceman;

        public DoneServicesViewModel(Func<UserControl, int> ucSetMethod, Serviceman user) : base(ucSetMethod)
        {
            serviceman = user;
            EntitySearcher.Collection = GetDataFromDB();
            ShowServicesButtonName = "Wszystkie";
        }

        public DoneServicesViewModel(Func<UserControl, int> ucSetMethod, IDbContext context) : base(ucSetMethod, context)
        {

        }

        /// <summary>
        /// Get all done services from db context
        /// </summary>
        public ObservableCollection<Service> GetDataFromDB()
        {
            ObservableCollection<Service> doneServices;
            using (var context = (Context == null ? new FiscalDbContext() : Context))
            {
                doneServices = new ObservableCollection<Service>(
                    context.Set<Service>().
                        Include("Device").
                        Include("Device.Place").
                        Include("Device.Client").
                        Include("TypeOfService").
                        Where(x => x.ExecutionTime.HasValue).
                        ToList());
            }

            return doneServices;
        }

        /// <summary>
        /// Get all done services from db context
        /// </summary>
        public ObservableCollection<Service> GetServicesWhichBelongToServiceman(Serviceman s)
        {
            ObservableCollection<Service> doneServices;
            using (var context = (Context == null ? new FiscalDbContext() : Context))
            {

                var query = (from service in context.Set<Service>()
                    join device in context.Set<Device>() on service.Device.ID equals device.ID
                    join repgroup in context.Set<RepairGroup>() on device.ID equals repgroup.Device.ID
                    where repgroup.Serviceman.ID == s.ID
                        select service).Include("Device").
                    Include("Device.Place").
                    Include("Device.Client").
                    Include("TypeOfService").ToList();
                doneServices = new ObservableCollection<Service>(query);
            }

            return doneServices;
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
        private ICommand _goToPlannedServicesButtonCommand;
        public ICommand GoToPlannedServicesButtonCommand
        {
            get
            {
                _goToPlannedServicesButtonCommand = new RelayCommand(o =>
                {
                    PlannedServices ps = new PlannedServices(UserControlSwitcher, serviceman);
                    UserControlSwitcher(ps);
                }, o => true);

                return _goToPlannedServicesButtonCommand;
            }
        }

        /// <summary>
        /// Go to done services
        /// </summary>
        private ICommand _goToShowAllServicesButtonCommand;
        public ICommand GoToShowAllServicesButtonCommand
        {
            get
            {
                _goToShowAllServicesButtonCommand = new RelayCommand(o =>
                {
                    if (ShowServicesButtonName == "Wszystkie")
                    {
                        EntitySearcher.Collection = GetDataFromDB();
                        ShowServicesButtonName = "Twoje";
                    }
                    else
                    {
                        EntitySearcher.Collection = GetServicesWhichBelongToServiceman(serviceman);
                        ShowServicesButtonName = "Wszystkie";
                    }
                  

                }, o => true);

                return _goToShowAllServicesButtonCommand;
            }
        }

        #region INotifyPropertyChanged things
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
