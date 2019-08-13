using Fiscal_Management_System.model.client;
using System.Collections.ObjectModel;

namespace Fiscal_Management_System.viewmodels.client
{
    public class ClientViewModel
    {
        private ClientService clientService;

        public Client Client
        {
            get;
            set;
        }

        public ObservableCollection<Client> Clients
        {
            get;
            set;
        }

        public ClientViewModel()
        {
            clientService = new ClientService();
            Clients = clientService.GetAll();
            Client = new Client() { Name = "Client's name" };
        }

    }
}
