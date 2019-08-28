using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.views.device;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fiscal_Management_System.viewmodels.place
{
    public class PlaceViewModel
    {
        /// <summary>
        /// Provides searching mechanisms
        /// </summary>
        private EntitySearcher<Place> _entitySearcher;
        public EntitySearcher<Place> EntitySearcher
        {
            get { return _entitySearcher; }
            set { _entitySearcher = value; }
        }

        private Place _place;
        public Place Place
        {
            get { return _place; }
            set { _place = value; }
        }

        private Client _client;
        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        private ICommand _goToClientDevicesButtonCommand;
        public ICommand GoToClientDevicesButtonCommand
        {
            get
            {
                _goToClientDevicesButtonCommand = new RelayCommand(o =>
                {
                    AllDevices dev = new AllDevices(Client, Place);
                    UserControlSwitcher(dev);
                }, o => true);

                return _goToClientDevicesButtonCommand;
            }
        }

        private ICommand _goToAddDeviceWithNewPlaceButtonCommand;
        public ICommand GoToAddDeviceWithNewPlaceButtonCommand
        {
            get
            {
                _goToAddDeviceWithNewPlaceButtonCommand = new RelayCommand(o =>
                {
                    AddOrEditDevice addDeviceWindow = new AddOrEditDevice(Client);
                    if ((bool)addDeviceWindow.ShowDialog())
                    {
                        MessageBox.Show("Dodano urządzenie z nowym miejscem instalacji!");
                        GetDataFromDB(Client);
                    }
                }, o => true);

                return _goToAddDeviceWithNewPlaceButtonCommand;
            }
        }

        public Func<UserControl, int> UserControlSwitcher;


        public PlaceViewModel(Client c, Func<UserControl, int> userControlSwitcher)
        {
            UserControlSwitcher = userControlSwitcher;
            Client = c;
            EntitySearcher = new EntitySearcher<Place>();
            GetDataFromDB(c);
        }

        private void GetDataFromDB(Client c)
        {
            using (var ctx = new FiscalDbContext())
            {
                c = ctx.Clients.Include("Devices").Include("Devices.Place").FirstOrDefault(x => x.ID == c.ID);
                HashSet<Place> places = new HashSet<Place>(c.Devices.ToList().Select(x => x.Place).ToList());
                EntitySearcher.Collection = new ObservableCollection<Place>(places);
            }

            if (EntitySearcher.Collection.Count == 0)
            {
                MessageBox.Show("Kontrahent nie posiada żadnych urządzeń. Dodaj urządzenie");
                AddOrEditDevice addDeviceWindow = new AddOrEditDevice(c);
                if ((bool)addDeviceWindow.ShowDialog())
                {
                    MessageBox.Show("Dodano urządzenie!");
                    using (var ctx = new FiscalDbContext())
                    {
                        Client clc = ctx.Clients.Include("Revenue").Include("Devices").Include("Devices.Place").Where(x => x.ID == c.ID).FirstOrDefault();
                        HashSet<Place> places = new HashSet<Place>(clc.Devices.ToList().Select(x => x.Place).ToList());
                        EntitySearcher.Collection = new ObservableCollection<Place>(places);
                    }
                }
            }
        }
    }
}
