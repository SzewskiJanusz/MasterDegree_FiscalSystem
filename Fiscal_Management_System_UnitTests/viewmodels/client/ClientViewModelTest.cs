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

namespace Fiscal_Management_System_UnitTests.viewmodels.client
{
    [TestClass]
    public class ClientViewModelTest : DbInitForTests
    {
        [TestMethod]
        public void GetAllClients_Test()
        {
            var mockSet = new Mock<DbSet<Client>>();
            var mockContext = new Mock<FiscalDbContext>();
            mockContext.Setup(m => m.Clients).Returns(mockSet.Object);
            ClientAddViewModel cavm = new ClientAddViewModel(mockContext.Object);
            cavm.Operation(new Client()
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
            });
            ClientViewModel cwm = new ClientViewModel(null, mockContext.Object);
            IEnumerable<Client> clients = cwm.GetDataFromDB(mockContext.Object);
            mockContext.Verify(m => m.Clients, Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

    }
}
