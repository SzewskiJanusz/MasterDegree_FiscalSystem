using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.views.client;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Fiscal_Management_System.viewmodels.client
{
    public class ClientViewModel
    {
        /// <summary>
        /// Provides searching mechanisms
        /// </summary>
        private EntitySearcher<Client> _entitySearcher;
        public EntitySearcher<Client> EntitySearcher
        {
            get { return _entitySearcher; }
            set { _entitySearcher = value; }
        }

        private Client _client;
        public Client Client { get { return _client; } set { _client = value; } }

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

        private ICommand _goToEditClientButtonCommand;
        public ICommand GoToEditClientButtonCommand
        {
            get
            {
                _goToEditClientButtonCommand = new RelayCommand(o =>
                {
                    Client client = new Client((Client)o);
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

        public ClientViewModel()
        {
            EntitySearcher = new EntitySearcher<Client>();
            GetDataFromDB();
        }

        private void GetDataFromDB()
        {
            using (var ctx = new FiscalDbContext())
            {
                EntitySearcher.Collection = new ObservableCollection<Client>(ctx.Clients.Include("Revenue"));
            }
        }
    }
}
