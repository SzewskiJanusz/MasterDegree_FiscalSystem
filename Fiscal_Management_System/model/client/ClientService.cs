using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.model.client
{
    public class ClientService
    {
        private static ObservableCollection<Client> oc = new ObservableCollection<Client>();

        public ObservableCollection<Client> GetAll()
        {
            oc.Add(new Client() { Name = "111" });
            oc.Add(new Client() { Name = "111" });
            oc.Add(new Client() { Name = "112" });
            oc.Add(new Client() { Name = "211" });
            return oc;
        }

        public void Add(Client c)
        {
            oc.Add(c);
        }
    }
}
