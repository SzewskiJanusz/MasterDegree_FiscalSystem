using Fiscal_Management_System.model.client;
using Fiscal_Management_System.views.client;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Fiscal_Management_System.viewmodels.client
{
    public class ClientViewModel
    {
        private Client _client;
        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        public ObservableCollection<Client> Clients
        {
            get;
            set;
        }

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
                        cs.Add((Client)o);

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

        public ClientViewModel()
        {
            ClientService cs = new ClientService();
            Clients = cs.GetAll();
            Client = new Client() { Name = "Client's name" };
        }

    }
}
