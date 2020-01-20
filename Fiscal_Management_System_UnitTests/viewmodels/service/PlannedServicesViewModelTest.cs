using System;
using System.Linq;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.viewmodels.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.service
{
    [TestClass]
    public class PlannedServicesViewModelTest
    {
        [TestMethod]
        public void GetPlannedServices_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Service>())
                .Returns(new FakeDbSet<Service>
                {
                    new Service() { ExecutionTime = new DateTime(2019,5,20), PlannedDateOfExecution = new DateTime()},
                    new Service() { ExecutionTime = null, PlannedDateOfExecution = new DateTime()},
                    new Service() { ExecutionTime = null, PlannedDateOfExecution = new DateTime()}
                });

            PlannedServicesViewModel dvm = new PlannedServicesViewModel(null, mock.Object);

            // Act
            var allPlannedServices = dvm.GetDataFromDB();

            // Assert
            Assert.AreEqual(2, allPlannedServices.Count());
        }
    }
}
