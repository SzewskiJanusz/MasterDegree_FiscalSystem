using System;
using System.Collections.Generic;
using Moq;
using System.Data.Entity;
using System.Linq;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.devicemodel;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.viewmodels.client;
using Fiscal_Management_System.viewmodels.device;
using Fiscal_Management_System.viewmodels.revenue;
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
                    ID = 1,
                    Name = "TestRevenue",
                    City = "TestCity",
                    Street = "TestStreet"
                }
            };

            var mockContext = new Mock<IDbContext>();
            var mockSet = new Mock<DbSet<Client>>();

            mockContext.Setup(m => m.Set<Client>()).Returns(mockSet.Object);

            mockContext.Setup(m => m.Set<Revenue>()).
                Returns(new FakeDbSet<Revenue>() { new Revenue() { ID = 1 } });

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

            var mockContext = new Mock<IDbContext>();
            var mockSet = new Mock<DbSet<Client>>();

            mockContext.Setup(m => m.Set<Client>()).Returns(mockSet.Object);

            mockContext.Setup(m => m.Set<Revenue>()).
                Returns(new FakeDbSet<Revenue>() { new Revenue() { ID = 1 } });

            ClientAddViewModel cavm = new ClientAddViewModel(mockContext.Object);
            cavm.Operation(client);

            mockSet.Verify(m => m.Add(It.IsAny<Client>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void AdditionOfClientWithRevenueTest()
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

            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(m => m.Set<Client>()).Returns(mockSet.Object);
            ClientAddViewModel cavm = new ClientAddViewModel(mockContext.Object);
            cavm.Operation(client);

            mockSet.Verify(m => m.Add(It.IsAny<Client>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
