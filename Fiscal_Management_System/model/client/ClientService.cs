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
            return oc;
        }

        public void Add(Client c)
        {
            oc.Add(c);
        }
    }
}
