using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.service;

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
                    context.Services.Include("Device").Include("Device.Place").Include("Device.Client").ToList());
            }

        }

        /// <summary>
        /// Add command
        /// </summary>
        private ICommand _goToAddServiceButtonCommand;
        public ICommand GoToAddServiceButtonCommand
        {
            get
            {
                _goToAddServiceButtonCommand = new RelayCommand(o =>
                {/*
                    AddOrEditService addServiceWindow = new AddOrEditService();
                    if ((bool)addServiceWindow.ShowDialog())
                    {
                        MessageBox.Show("Dodano usługę!");
                      //  GetDataFromDB();
                    }*/
                }, o => true);

                return _goToAddServiceButtonCommand;
            }
        }

        /// <summary>
        /// Edit command
        /// </summary>
        private ICommand _goToEditServiceButtonCommand;
        public ICommand GoToEditServiceButtonCommand
        {
            get
            {
                _goToEditServiceButtonCommand = new RelayCommand(o =>
                {
                    Service serv = new Service((Service)o);/*
                    AddOrEditService editServiceWindow = new AddOrEditService(serv);
                    if ((bool)editServiceWindow.ShowDialog())
                    {
                        MessageBox.Show("Usługa poprawiona!");
                      //  GetDataFromDB();
                    }*/
                }, o => true);

                return _goToEditServiceButtonCommand;
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
                    
                }, o => true);

                return _goToServiceDetailsButtonCommand;
            }
        }
    }
}
