using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.model.client
{
    public class ClientService
    {
        public ObservableCollection<Client> GetAll()
        {
            ObservableCollection<Client> data = new ObservableCollection<Client>
            {
                new Client() { ID = 1, NIP = "123123123", Name = "Aaa" }
            };
            return data;
        }
    }
}
