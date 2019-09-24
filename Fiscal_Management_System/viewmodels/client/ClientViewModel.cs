using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.views.client;
using Fiscal_Management_System.views.place;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fiscal_Management_System.viewmodels.client
{
    public class ClientViewModel : EntityViewModel<Client>
    {
        /// <summary>
        /// Add command
        /// </summary>
        private ICommand _goToAddClientButtonCommand;
        public ICommand GoToAddClientButtonCommand
        {
            get
            {
                _goToAddClientButtonCommand = new RelayCommand(o =>
                {
                    AddOrEditClient addClientWindow = new AddOrEditClient();
                    if ((bool)addClientWindow.ShowDialog())
                    {
                        MessageBox.Show("Dodano kontrahenta!");
                        GetDataFromDB();
                    }
                }, o => true);

                return _goToAddClientButtonCommand;
            }
        }

        /// <summary>
        /// Edit command
        /// </summary>
        private ICommand _goToEditClientButtonCommand;
        public ICommand GoToEditClientButtonCommand
        {
            get
            {
                _goToEditClientButtonCommand = new RelayCommand(o =>
                {
                    Client client= new Client((Client)o);
                    AddOrEditClient editClientWindow = new AddOrEditClient(client);
                    if ((bool)editClientWindow.ShowDialog())
                    {
                        MessageBox.Show("Kontrahent poprawiony!");
                        GetDataFromDB();
                    }
                }, o => true);

                return _goToEditClientButtonCommand;
            }
        }


        /// <summary>
        /// Go to client's installation places command
        /// </summary>
        private ICommand _goToClientPlacesButtonCommand;
        public ICommand GoToClientPlacesButtonCommand
        {
            get
            {
                _goToClientPlacesButtonCommand = new RelayCommand(o =>
                {
                    Client client = new Client((Client)o);
                    UserControlSwitcher(new ClientDevicePlaces(client, UserControlSwitcher));
                }, o => true);

                return _goToClientPlacesButtonCommand;
            }
        }

        public ClientViewModel(Func<UserControl, int> ucSetMethod) : base(ucSetMethod)
        {
            UserControlSwitcher = ucSetMethod;
            EntitySearcher = new EntitySearcher<Client>();
            GetDataFromDB();
        }

        public ClientViewModel(Func<UserControl, int> ucSetMethod, IDbContext context) : base(ucSetMethod)
        {
            EntitySearcher = new EntitySearcher<Client>();
        }

        public IEnumerable<Client> GetDataFromDB(IDbContext context)
        {
            EntitySearcher.Collection = new ObservableCollection<Client>
            (
                    context.Set<Client>()
                    .Include("Revenue")
                    .Include("Devices")
                    .Include("Devices.Place")
            );
            return EntitySearcher.Collection;
        }

        private void GetDataFromDB()
        {
            using (var ctx = new FiscalDbContext())
            {
                EntitySearcher.Collection = new ObservableCollection<Client>
                (
                        ctx.Clients
                        .Include("Revenue")
                        .Include("Devices")
                        .Include("Devices.Place")
                );
            }
        }

    }
}
