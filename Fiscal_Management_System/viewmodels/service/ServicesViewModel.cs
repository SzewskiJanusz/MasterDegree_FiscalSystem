using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.views.service;

namespace Fiscal_Management_System.viewmodels.service
{
    public class ServicesViewModel : EntityViewModel<Service>
    {
        public ServicesViewModel(Func<UserControl, int> ucSetMethod) : base(ucSetMethod)
        {
            GetDataFromDB();
        }

        public void GetDataFromDB()
        {
            using (var context = new FiscalDbContext())
            {
                EntitySearcher.Collection = new ObservableCollection<Service>(
                    context.Services.
                        Include("Device").
                        Include("Device.Place").
                        Include("Device.Client").
                        Include("TypeOfService").
                        ToList());
            }
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
    }
}
