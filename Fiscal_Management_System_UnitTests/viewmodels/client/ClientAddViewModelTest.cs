using System;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
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

            var mockSet = new Mock<DbSet<Client>>();
            var mockContext = new Mock<FiscalDbContext>();
            mockContext.Setup(m => m.Clients).Returns(mockSet.Object);
            ClientAddViewModel cavm = new ClientAddViewModel(mockContext.Object);
            cavm.Operation(client);

            mockSet.Verify(m => m.Add(It.IsAny<Client>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AdditionOfClientTest_RevenueIsNull()
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
                RevenueId = 0,
                Revenue = null
            };

            var mockSet = new Mock<DbSet<Client>>();
            var mockContext = new Mock<FiscalDbContext>();
            mockContext.Setup(m => m.Clients).Returns(mockSet.Object);
            ClientAddViewModel cavm = new ClientAddViewModel(mockContext.Object);
            cavm.Operation(client);

            mockSet.Verify(m => m.Add(It.IsAny<Client>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
