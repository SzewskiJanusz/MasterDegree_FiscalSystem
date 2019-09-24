using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.viewmodels.client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.viewmodels.device;

namespace Fiscal_Management_System_UnitTests.viewmodels.client
{
    [TestClass]
    public class ClientViewModelTest : DbInitForTests
    {
        [TestMethod]
        public void GetAllClients_Test()
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

            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Client>())
                .Returns(new FakeDbSet<Client>
                {
                    client
                });
            // Act
            ClientViewModel dvm = new ClientViewModel(null, mock.Object);


            var allClients = dvm.GetDataFromDB(mock.Object);

            // Assert
            Assert.AreEqual(1, allClients.Count());
        }

    }
}
