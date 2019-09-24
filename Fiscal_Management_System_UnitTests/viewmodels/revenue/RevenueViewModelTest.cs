using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.viewmodels.client;
using Fiscal_Management_System.viewmodels.device;
using Fiscal_Management_System.viewmodels.revenue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.revenue
{
    [TestClass]
    public class RevenueViewModelTest : DbInitForTests
    {
        [TestMethod]
        public void GetAllRevenues_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Revenue>())
                .Returns(new FakeDbSet<Revenue>
                {
                    new Revenue() { Name = "I Urząd Skarbowy testowy", City = "Testowe", Street = "Testowa"}
                });
            
            RevenueViewModel dvm = new RevenueViewModel(null, mock.Object);

            // Act
            var allRevenues = dvm.GetAllRevenues();

            // Assert
            Assert.AreEqual(1, allRevenues.Count());
        }
    }
}
