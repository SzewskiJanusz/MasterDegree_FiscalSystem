using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.viewmodels.revenue;
using Fiscal_Management_System.viewmodels.typeofservice;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.typeofservice
{
    [TestClass]
    public class TypeOfServiceViewModelTest : DbInitForTests
    {
        [TestMethod]
        public void GetAllTypesOfServices_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<TypeOfService>())
                .Returns(new FakeDbSet<TypeOfService>
                {
                    new TypeOfService() { Name = "TestTyp"}
                });

            TypesOfServicesViewModel dvm = new TypesOfServicesViewModel(null, mock.Object);

            // Act
            var allServices = dvm.GetAllTypesOfServices();

            // Assert
            Assert.AreEqual(1, allServices.Count());
        }
    }
}
