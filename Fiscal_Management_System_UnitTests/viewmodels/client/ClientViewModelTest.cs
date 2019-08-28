using Fiscal_Management_System.model.client;
using Fiscal_Management_System.viewmodels.client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiscal_Management_System_UnitTests.viewmodels.client
{
    [TestClass]
    public class ClientViewModelTest
    {
        [TestMethod]
        public void GetAllClients_Test()
        {
            ClientViewModel cwm = new ClientViewModel(null);
            Assert.AreNotEqual(0, cwm.EntitySearcher.Collection.Count);
        }

    }
}
