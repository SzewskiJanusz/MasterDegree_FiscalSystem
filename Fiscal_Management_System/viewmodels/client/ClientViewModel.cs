using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.views.client;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// Binding client with datagrid and textboxes during addition
        /// </summary>
        private Client _client;
        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        /// <summary>
        /// Collection used to view data in datagrid
        /// </summary>
        public ObservableCollection<Client> Clients
        {
            get;
            set;
        }

        public string ButtonText { get; set; }
        public string WindowTitle { get; set; }

        private ICommand _confirmAdditionOfClientButtonCommand;
        public ICommand ConfirmAdditionOfClientButtonCommand
        {
            get
            {
                if (_confirmAdditionOfClientButtonCommand == null)
                {
                    _confirmAdditionOfClientButtonCommand = new RelayCommand(o =>
                    {
                        ClientService cs = new ClientService();
                        Client unboundClient = new Client((Client)o);
                        if (ButtonText == "OK")
                        {
                            cs.Edit(unboundClient);
                        }
                        else
                        {
                            cs.Add(unboundClient);
                        }
                    }, o => true);
                }

                return _confirmAdditionOfClientButtonCommand;
            }
        }

        private ICommand _goToAddClientButtonCommand;
        public ICommand GoToAddClientButtonCommand
        {
            get
            {
                _goToAddClientButtonCommand = new RelayCommand(o =>
                {
                    AddOrEditClient addClientWindow = new AddOrEditClient();
                    addClientWindow.Show();
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
                    Client unboundClient = new Client((Client)o);
                    AddOrEditClient editClientWindow = new AddOrEditClient(unboundClient);
                    editClientWindow.Show();
                }, o => true);

                return _goToEditClientButtonCommand;
            }
        }

        public ClientViewModel()
        {
            ClientService cs = new ClientService();
            Clients = cs.GetAll();
            EntitySearcher = new EntitySearcher<Client>(Clients);
            Client = new Client() { Name = "Client's name" };
            ButtonText = "Dodaj";
            WindowTitle = "Dodawanie kontrahenta";
        }

        public ClientViewModel(Client c)
        {
            ClientService cs = new ClientService();
            Clients = cs.GetAll();
            EntitySearcher = new EntitySearcher<Client>(Clients);
            Client = new Client(c);
            ButtonText = "OK";
            WindowTitle = "Edytowanie kontrahenta";
        }
    }
}
