using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.views.device;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fiscal_Management_System.viewmodels.place
{
    public class PlaceViewModel
    {

        private IDbContext Context;
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

        public PlaceViewModel(Client c, IDbContext context, Func<UserControl, int> userControlSwitcher)
        {
            Context = context;
            UserControlSwitcher = userControlSwitcher;
            Client = c;
            EntitySearcher = new EntitySearcher<Place>();
        }

        private void GetDataFromDB(Client c)
        {
            EntitySearcher.Collection = GetClientPlacesFromDb(c);

            if (EntitySearcher.Collection.Count == 0)
            {
                MessageBox.Show("Kontrahent nie posiada żadnych urządzeń. Dodaj urządzenie");
                AddOrEditDevice addDeviceWindow = new AddOrEditDevice(c);
                if ((bool)addDeviceWindow.ShowDialog())
                {
                    MessageBox.Show("Dodano urządzenie!");
                    EntitySearcher.Collection = GetClientPlacesFromDb(c);
                }
            }
        }

        public ObservableCollection<Place> GetClientPlacesFromDb(Client c)
        {
            HashSet<Place> places;
            using (var ctx = Context == null ? new FiscalDbContext() : Context)
            {
                c = ctx.Set<Client>().Include("Devices").Include("Devices.Place").Include("Revenue").FirstOrDefault(x => x.ID == c.ID);
                places = new HashSet<Place>(c.Devices.ToList().Select(x => x.Place).ToList());
            }
            return new ObservableCollection<Place>(places);
        }
    }
}
