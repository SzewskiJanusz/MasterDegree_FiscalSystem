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

            var mockContext = new Mock<FiscalDbContext>();
            var mockSet = new Mock<DbSet<Client>>();

            mockSet.Object.Add(client);
            mockContext.Setup(m => m.Clients).Returns(mockSet.Object);


            ClientEditViewModel cevm = new ClientEditViewModel(mockContext.Object, client);
            client.NIP = "1111111111";
            cevm.Operation(client);

            Assert.AreEqual("1111111111", mockSet.Object.FirstOrDefault().NIP);
        }

    }
}
