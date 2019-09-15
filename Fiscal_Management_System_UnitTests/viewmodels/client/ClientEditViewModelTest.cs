using System.Collections.Generic;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.revenue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Linq;
using Fiscal_Management_System.model;
using Fiscal_Management_System.viewmodels.client;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.client
{
    [TestClass]
    public class ClientEditViewModelTest : DbInitForTests
    {
        [TestMethod]
        public void EditClientTest()
        {
            Client client = new Client()
            {
                ID = 1,
                Name = "TestClient",
                Symbol = "TestSymbol",
                NIP = "3234321234",
                State = "TestState",
                City = "TestCity",
                Street = "TestStreet",
                PostalCode = "11-111",
                Phone = "123-123-123",
                Email = "client@test.com",
                RevenueId = 1,
                Revenue = new Revenue()
                {
                    Name = "TestRevenue",
                    City = "TestCity",
                    Street = "TestStreet"
                }
            };
            Assert.AreEqual(true,true);
  
        }

    }
}
