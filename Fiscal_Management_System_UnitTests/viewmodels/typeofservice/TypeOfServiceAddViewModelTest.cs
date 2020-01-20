using System.Data.Entity;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.viewmodels.typeofservice;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.typeofservice
{
    [TestClass]
    public class TypeOfServiceAddViewModelTest : DbInitForTests
    {
        [TestMethod]
        public void AddRevenue_Test()
        {
            TypeOfService tos = new TypeOfService()
            {
                Id = 1,
                Name = "TestServ",
            };

            var mockContext = new Mock<IDbContext>();
            var mockSet = new Mock<IDbSet<TypeOfService>>();

            mockContext.Setup(m => m.Set<TypeOfService>()).Returns(mockSet.Object);

            TypeOfServiceAddViewModel cavm = new TypeOfServiceAddViewModel(mockContext.Object);
            cavm.Operation(tos);

            mockSet.Verify(m => m.Add(It.IsAny<TypeOfService>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
