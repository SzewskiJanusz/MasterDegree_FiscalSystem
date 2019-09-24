using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.viewmodels.client;
using Fiscal_Management_System.viewmodels.revenue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.revenue
{
    [TestClass]
    public class RevenueAddViewModelTest : DbInitForTests
    {
        [TestMethod]
        public void AddRevenue_Test()
        {
            Revenue revenue = new Revenue()
            {
                ID = 1,
                Name = "TestRevenue",
                City = "TestCity",
                Street = "TestStreet"
            };

            var mockContext = new Mock<IDbContext>();
            var mockSet = new Mock<IDbSet<Revenue>>();

            mockContext.Setup(m => m.Set<Revenue>()).Returns(mockSet.Object);

            RevenueAddViewModel cavm = new RevenueAddViewModel(mockContext.Object);
            cavm.Operation(revenue);

            mockSet.Verify(m => m.Add(It.IsAny<Revenue>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
