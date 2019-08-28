using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.views.device;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Fiscal_Management_System.viewmodels.device
{
    public class DeviceViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Provides searching mechanisms
        /// </summary>
        private EntitySearcher<Device> _entitySearcher;
        public EntitySearcher<Device> EntitySearcher
        {
            get { return _entitySearcher; }
            set { _entitySearcher = value; }
        }

        private Device _device;
        public Device Device { get { return _device; } set { _device = value; } }

        private Place _place;
        public Place Place { get { return _place; } set { _place = value; } }

        public string PlaceOfDevice { get { return Place.City + " " + Place.Street + " " + Place.State; } }

        private Client _client;
        public Client Client { get { return _client; } set { _client = value; OnPropertyChanged("Client"); } }

        private ICommand _goToAddDeviceButtonCommand;
        public ICommand GoToAddDeviceButtonCommand
        {
            get
            {
                _goToAddDeviceButtonCommand = new RelayCommand(o =>
                {
                    AddOrEditDevice addDeviceWindow = new AddOrEditDevice(Client, Place);
                    if ((bool)addDeviceWindow.ShowDialog())
                    {
                        MessageBox.Show("Dodano urządzenie!");
                        GetDataFromDB(Client, Place);
                    }
                }, o => true);

                return _goToAddDeviceButtonCommand;
            }
        }

        private ICommand _goToEditDeviceButtonCommand;
        public ICommand GoToEditDeviceButtonCommand
        {
            get
            {
                _goToEditDeviceButtonCommand = new RelayCommand(o =>
                {
                    Device device = new Device(Device);
                    AddOrEditDevice editDeviceWindow = new AddOrEditDevice(device, Client, Place);
                    if ((bool)editDeviceWindow.ShowDialog())
                    {
                        MessageBox.Show("Urządzenie poprawione!");
                        GetDataFromDB(Client, Place);
                    }
                }, o => true);

                return _goToEditDeviceButtonCommand;
            }
        }


        private ICommand _goToLiquidateDeviceButtonCommand;
        public ICommand GoToLiquidateDeviceButtonCommand
        {
            get
            {
                _goToLiquidateDeviceButtonCommand = new RelayCommand(o =>
                {
                    MessageBoxResult result = 
                    MessageBox.Show("Czy na pewno chcesz zlikwidować to urządzenie (nr.unikatowy: "+Device.UniqueNumber+"? Operacja jest nieodwracalna",
                        "Ostrzeżenie", 
                        MessageBoxButton.YesNoCancel
                        );

                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBoxResult result2 = MessageBox.Show("Czy jesteś pewien?","Potwierdzenie",
                            MessageBoxButton.YesNo);
                        if (result2 == MessageBoxResult.Yes)
                        {
                            using (var ctx = new FiscalDbContext())
                            {
                                ctx.Devices.FirstOrDefault(x => x.ID == Device.ID).DateOfLiquidation = DateTime.Today;
                                ctx.SaveChanges();
                                GetDataFromDB(Client, Place);
                            }
                        }
                    }

                }, o => true);

                return _goToLiquidateDeviceButtonCommand;
            }
        }

        
        private ICommand _showHideLiquidatedButtonCommand;
        public ICommand ShowHideLiquidatedButtonCommand
        {
            get
            {
                _showHideLiquidatedButtonCommand = new RelayCommand(o =>
                {
                    if (ShowHideLiquidatedButtonText == "Pokaż zlikwidowane")
                    {
                        using (var ctx = new FiscalDbContext())
                        {
                            if (Client == null && Place == null)
                            {
                                EntitySearcher.Collection =
                                new ObservableCollection<Device>(ctx.Devices.Include("Client").Include("Place").Include("Model").ToList());
                            }
                            else if (Client != null && Place == null)
                            {
                                EntitySearcher.Collection =
                               new ObservableCollection<Device>(ctx.Devices.Include("Client").
                               Include("Place").Include("Model").Where(x => x.Client.ID == Client.ID).ToList());
                            }
                            else
                            {
                                EntitySearcher.Collection =
                                new ObservableCollection<Device>(
                                    ctx.Devices.Include("Client").Include("Place").Include("Model").
                                    Where(x => x.Client.ID == Client.ID && x.Place.ID == Place.ID).ToList());
                            }
                            ShowHideLiquidatedButtonText = "Ukryj zlikwidowane";
                        }
                    }
                    else
                    {
                        using (var ctx = new FiscalDbContext())
                        {
                            if (Client == null && Place == null)
                            {
                                GetDataFromDB();
                            }
                            else if (Client != null && Place == null)
                            {
                                GetDataFromDB(Client);
                            }
                            else
                            {
                                GetDataFromDB(Client, Place);
                            }
                            ShowHideLiquidatedButtonText = "Pokaż zlikwidowane";
                        }

                    }
                    
                }, o => true);

                return _showHideLiquidatedButtonCommand;
            }
        }

       

        private string _showHideLiquidatedButtonText;
        public string ShowHideLiquidatedButtonText
        { get { return _showHideLiquidatedButtonText; } set { _showHideLiquidatedButtonText = value; OnPropertyChanged("ShowHideLiquidatedButtonText"); } }

        public DeviceViewModel()
        {
            EntitySearcher = new EntitySearcher<Device>();
            ShowHideLiquidatedButtonText = "Pokaż zlikwidowane";
            GetDataFromDB();
        }

        public DeviceViewModel(Client c)
        {
            EntitySearcher = new EntitySearcher<Device>();
            ShowHideLiquidatedButtonText = "Pokaż zlikwidowane";
            Client = c;
            GetDataFromDB(c);
        }

        public DeviceViewModel(Client c, Place p)
        {
            EntitySearcher = new EntitySearcher<Device>();
            ShowHideLiquidatedButtonText = "Pokaż zlikwidowane";
            Client = c;
            Place = p;
            GetDataFromDB(c, p);
        }

        private void GetDataFromDB()
        {
            using (var ctx = new FiscalDbContext())
            {
                EntitySearcher.Collection = new ObservableCollection<Device>(ctx.Devices.Include("Client").Include("Place").Include("Model").Where(x=> !x.DateOfLiquidation.HasValue).ToList());
            }
        }

        private void GetDataFromDB(Client c)
        {
            Client = c;
            using (var ctx = new FiscalDbContext())
            {
                EntitySearcher.Collection =
                    new ObservableCollection<Device>(ctx.Devices.Include("Client").Include("Place").Include("Model").
                    Where(x => x.ClientId == c.ID && !x.DateOfLiquidation.HasValue).ToList());
            }
        }

        private void GetDataFromDB(Client c, Place p)
        {
            Client = c;
            using (var ctx = new FiscalDbContext())
            {
                Place pl = ctx.Places.FirstOrDefault(x => x.State == p.State && x.City == p.City && x.Street == p.Street);

                EntitySearcher.Collection =
                    new ObservableCollection<Device>(ctx.Devices.Include("Client").Include("Place").Include("Model").
                    Where(x => x.Client.ID == c.ID && x.Place.ID == pl.ID && !x.DateOfLiquidation.HasValue).ToList());
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
