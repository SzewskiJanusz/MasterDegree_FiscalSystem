using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.views.device;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Fiscal_Management_System.viewmodels.device
{
    public class DeviceViewModel : EntityViewModel<Device>, INotifyPropertyChanged
    {
        private Device _device;
        public Device Device { get { return _device; } set { _device = value; } }

        private Place _place;
        public Place Place { get { return _place; } set { _place = value; } }

        public string PlaceOfDevice { get { return Place != null ? Place.City + " " + Place.Street + " " + Place.State : null; } }

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
                        EntitySearcher.Collection = GetDataFromDb(Client, Place);
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
                        EntitySearcher.Collection = GetDataFromDb(Client, Place);
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
                            using (var ctx = Context == null ? new FiscalDbContext() : Context)
                            {
                                ctx.Set<Device>().FirstOrDefault(x => x.ID == Device.ID).DateOfLiquidation = DateTime.Today;
                                ctx.SaveChanges();
                                EntitySearcher.Collection = GetDataFromDb(Client, Place);
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
                        if (Client == null && Place == null)
                        {
                            EntitySearcher.Collection = GetAllDevicesFromDb();
                        }
                        else if (Client != null && Place == null)
                        {
                            EntitySearcher.Collection = GetAllDevicesFromDb(Client);
                        }
                        else
                        {
                            EntitySearcher.Collection = GetAllDevicesFromDb(Client, Place);
                        }

                        ShowHideLiquidatedButtonText = "Ukryj zlikwidowane";
                    }
                    else
                    {
                        if (Client == null && Place == null)
                        {
                            EntitySearcher.Collection = GetDataFromDb();
                        }
                        else if (Client != null && Place == null)
                        {
                            EntitySearcher.Collection = GetDataFromDb(Client);
                        }
                        else
                        {
                            EntitySearcher.Collection = GetDataFromDb(Client, Place);
                        }
                        
                        ShowHideLiquidatedButtonText = "Pokaż zlikwidowane";
                    }
                    
                }, o => true);

                return _showHideLiquidatedButtonCommand;
            }
        }

        private string _showHideLiquidatedButtonText;
        public string ShowHideLiquidatedButtonText
        { get { return _showHideLiquidatedButtonText; } set { _showHideLiquidatedButtonText = value; OnPropertyChanged("ShowHideLiquidatedButtonText"); } }

        public DeviceViewModel() : base(null)
        {
            ShowHideLiquidatedButtonText = "Pokaż zlikwidowane";
            EntitySearcher.Collection = GetDataFromDb();
        }

        public DeviceViewModel(Client c) : base(null)
        {
            ShowHideLiquidatedButtonText = "Pokaż zlikwidowane";
            Client = c;
            EntitySearcher.Collection = GetDataFromDb(c);
        }

        public DeviceViewModel(Client c, Place p) : base(null)
        {
            ShowHideLiquidatedButtonText = "Pokaż zlikwidowane";
            Client = c;
            Place = p;
            EntitySearcher.Collection = GetDataFromDb(c, p);
        }

        /// <summary>
        /// Constructor for testing
        /// </summary>
        /// <param name="ForTestPurposes"></param>
        /// <param name="context"></param>
        public DeviceViewModel(IDbContext context) : base(null, context)
        {
            ShowHideLiquidatedButtonText = "Pokaż zlikwidowane";
        }

        /// <summary>
        /// All devices
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Device> GetDataFromDb()
        {
            List<Device> devices;
            using (var ctx = Context == null ? new FiscalDbContext() : Context)
            {
                devices = ctx.Set<Device>().Include("Client").Include("Place").Include("Model").
                    Where(x=> !x.DateOfLiquidation.HasValue).ToList();
            }
            return new ObservableCollection<Device>(devices);
        }

        /// <summary>
        /// Client's devices
        /// </summary>
        /// <param name="c"></param>
        public ObservableCollection<Device> GetDataFromDb(Client c)
        {
            Client = c;
            List<Device> devices;
            using (var ctx = Context == null ? new FiscalDbContext() : Context)
            {
                devices = ctx.Set<Device>().Include("Client").Include("Place").Include("Model").
                    Where(x => x.ClientId == c.ID && !x.DateOfLiquidation.HasValue).ToList();
            }
            return new ObservableCollection<Device>(devices);
        }

        /// <summary>
        /// Client's devices in place
        /// </summary>
        /// <param name="c"></param>
        /// <param name="p"></param>
        public ObservableCollection<Device> GetDataFromDb(Client c, Place p)
        {
            Client = c;
            List<Device> devices;
            using (var ctx = Context == null ? new FiscalDbContext() : Context)
            {
                Place pl = ctx.Set<Place>().FirstOrDefault(x => x.State == p.State && x.City == p.City && x.Street == p.Street);
                devices = ctx.Set<Device>().Include("Client").Include("Place").Include("Model").Where(x =>
                    x.Client.ID == c.ID && x.Place.ID == pl.ID && !x.DateOfLiquidation.HasValue).ToList();

            }
            return new ObservableCollection<Device>(devices);
        }

        public ObservableCollection<Device> GetAllDevicesFromDb()
        {
            List<Device> devices;
            using (var ctx = Context == null ? new FiscalDbContext() : Context)
            {
                devices = ctx.Set<Device>().Include("Client").Include("Place").Include("Model").ToList();
            }
            return new ObservableCollection<Device>(devices);
        }

        public ObservableCollection<Device> GetAllDevicesFromDb(Client c)
        {
            List<Device> devices;
            using (var ctx = Context == null ? new FiscalDbContext() : Context)
            {
                devices = ctx.Set<Device>().Include("Client").Include("Place").Include("Model")
                    .Where(x => x.ClientId == c.ID).ToList();

                EntitySearcher.Collection = new ObservableCollection<Device>(devices);
            }
            return new ObservableCollection<Device>(devices);
        }

        public ObservableCollection<Device> GetAllDevicesFromDb(Client c, Place p)
        {
            List<Device> devices;
            using (var ctx = Context == null ? new FiscalDbContext() : Context)
            {
                devices = ctx.Set<Device>().Include("Client").Include("Place").Include("Model")
                    .Where(x => x.ClientId == c.ID && x.PlaceId == p.ID).ToList();

                EntitySearcher.Collection = new ObservableCollection<Device>(devices);
            }
            return new ObservableCollection<Device>(devices);
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
