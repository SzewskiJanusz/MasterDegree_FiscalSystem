using System;
using System.Linq;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.viewmodels.client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fiscal_Management_System_UnitTests.viewmodels.client
{
    [TestClass]
    public class ClientAddViewModelTest : DbInitForTests
    {
        [TestMethod]
        public void AdditionOfClientTest()
        {
            ClientAddViewModel cvm = new ClientAddViewModel();
            Client client = new Client()
            {
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

            cvm.Operation(client);

            bool exists;
            using (FiscalDbContext ctx = new FiscalDbContext())
            {
                exists = ctx.Revenues.Include("Revenue").Any(x => x.ID == client.ID);
            }

            Assert.IsTrue(exists);
        }
    }
}
