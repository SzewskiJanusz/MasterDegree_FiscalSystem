using Fiscal_Management_System.views.client;
using Fiscal_Management_System.views.device;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Fiscal_Management_System.model;
using Fiscal_Management_System.views.devicemodel;
using Fiscal_Management_System.views.revenue;
using Fiscal_Management_System.views.service;
using Fiscal_Management_System.views.typeofservice;

namespace Fiscal_Management_System.viewmodels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private UserControl _userControl;
        public UserControl UserControl
        {
            get { return _userControl; }
            set { _userControl = value; OnPropertyChanged("UserControl"); }
        }

        private List<UserControl> _previousUserControls;
        public List<UserControl> PreviousUserControls
        {
            get { return _previousUserControls; }
            set { _previousUserControls = value; OnPropertyChanged("PreviousUserControls"); }
        }

        private List<UserControl> _nextUserControls;
        public List<UserControl> NextUserControls
        {
            get { return _nextUserControls; }
            set { _nextUserControls = value; OnPropertyChanged("NextUserControls"); }
        }

        private ICommand _setPreviousUserControlButtonCommand;
        public ICommand SetPreviousUserControlButtonCommand
        {
            get
            {
                _setPreviousUserControlButtonCommand = new RelayCommand(o =>
                {
                    SetLastUserControl(null);
                }, o => true);

                return _setPreviousUserControlButtonCommand;
            }
        }

        private ICommand _setNextUserControlButtonCommand;
        public ICommand SetNextUserControlButtonCommand
        {
            get
            {
                _setNextUserControlButtonCommand = new RelayCommand(o =>
                {
                    SetNextUserControl(null);
                }, o => true);

                return _setNextUserControlButtonCommand;
            }
        }

        private ICommand _goToClientsButtonCommand;
        public ICommand GoToClientsButtonCommand
        {
            get
            {
                _goToClientsButtonCommand = new RelayCommand(o =>
                {
                    SetUserControl(new AllClients(SetUserControl));
                }, o => true);

                return _goToClientsButtonCommand;
            }
        }


        private Serviceman _loggedUser;
        public Serviceman LoggedUser
        {
            get { return _loggedUser; }
            set { _loggedUser = value; OnPropertyChanged("LoggedUser"); }
        }

        public string LoggedInformation { get; set; }

        private ICommand _goToDevicesButtonCommand;
        public ICommand GoToDevicesButtonCommand
        {
            get
            {
                _goToDevicesButtonCommand = new RelayCommand(o =>
                {
                    SetUserControl(new AllDevices());
                }, o => true);

                return _goToDevicesButtonCommand;
            }
        }

        private ICommand _goToServicesButtonCommand;
        public ICommand GoToServicesButtonCommand
        {
            get
            {
                _goToServicesButtonCommand = new RelayCommand(o =>
                {
                    SetUserControl(new PlannedServices(SetUserControl, LoggedUser));
                }, o => true);

                return _goToServicesButtonCommand;
            }
        }

        private ICommand _goToRevenuesCommand;
        public ICommand GoToRevenuesCommand
        {
            get
            {
                _goToRevenuesCommand = new RelayCommand(o =>
                {
                    SetUserControl(new AllRevenues(SetUserControl));
                }, o => true);

                return _goToRevenuesCommand;
            }
        }

        private ICommand _goToDeviceModelsCommand;
        public ICommand GoToDeviceModelsCommand
        {
            get
            {
                _goToDeviceModelsCommand = new RelayCommand(o =>
                {
                    SetUserControl(new AllDeviceModels(SetUserControl));
                }, o => true);

                return _goToDeviceModelsCommand;
            }
        }

        private ICommand _goToTypesOfServicesCommand;
        public ICommand GoToTypesOfServicesCommand
        {
            get
            {
                _goToTypesOfServicesCommand = new RelayCommand(o =>
                {
                    SetUserControl(new AllTypesOfServices(SetUserControl));
                }, o => true);

                return _goToTypesOfServicesCommand;
            }
        }
        
        public int SetNextUserControl(UserControl uc)
        {
            if (NextUserControls.Count > 0)
            {
                PreviousUserControls.Add(UserControl);
                UserControl = NextUserControls.Last();
                NextUserControls.RemoveAt(NextUserControls.Count - 1);
            }
            return 1;
        }

        public int SetLastUserControl(UserControl uc)
        {
            if (PreviousUserControls.Count > 0)
            {
                NextUserControls.Add(UserControl);
                UserControl = PreviousUserControls.Last();
                PreviousUserControls.RemoveAt(PreviousUserControls.Count - 1);
            }
            return 1;
        }

        public int SetUserControl(UserControl uc) 
        {
            PreviousUserControls.Add(UserControl);
            UserControl = uc;
            return 1;
        }

        public MainViewModel(Serviceman user)
        {
            LoggedUser = user;
            LoggedInformation = "Zalogowano jako: " + user.NameAndSurname;
           
            // Init navigation lists
            PreviousUserControls = new List<UserControl>();
            NextUserControls = new List<UserControl>();

            // Default value
            AllClients ac = new AllClients(SetUserControl);
            ac.Tag = "Clients";
            UserControl = ac;
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
