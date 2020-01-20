using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.repairgroup;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.viewmodels.revenue;
using Fiscal_Management_System.viewmodels.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.service
{
    [TestClass]
    public class DoneServicesViewModelTest
    {
        [TestMethod]
        public void GetDoneServices_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Service>())
                .Returns(new FakeDbSet<Service>
                {
                    new Service() { ExecutionTime = new DateTime(2019,5,20), PlannedDateOfExecution = new DateTime()},
                    new Service() { ExecutionTime = null, PlannedDateOfExecution = new DateTime()}
                });

            DoneServicesViewModel dvm = new DoneServicesViewModel(null, mock.Object);

            // Act
            var allDoneServices = dvm.GetDataFromDB();

            // Assert
            Assert.AreEqual(1, allDoneServices.Count());
        }

        [TestMethod]
        public void GetDoneServicesOfServiceman_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Service>())
                .Returns(new FakeDbSet<Service>
                {
                    new Service() { ExecutionTime = new DateTime(2019,5,20), PlannedDateOfExecution = new DateTime(),
                        Device = new Device(){ ID = 1, RepairGroups = new List<RepairGroup>(){new RepairGroup(){Serviceman = new Serviceman(){ID = 1}}}}},
                });

            mock.Setup(x => x.Set<Device>())
                .Returns(new FakeDbSet<Device>
                {
                    new Device() {ID = 1}
                });

            mock.Setup(x => x.Set<RepairGroup>())
                .Returns(new FakeDbSet<RepairGroup>
                {
                    new RepairGroup(){Serviceman = new Serviceman(){ID = 1}, Device = new Device(){ID = 1}}
                });

            DoneServicesViewModel dvm = new DoneServicesViewModel(null, mock.Object);

            // Act
            var allDoneServicesOfServiceman = dvm.GetServicesWhichBelongToServiceman(new Serviceman() {ID = 1});

            // Assert
            Assert.AreEqual(1, allDoneServicesOfServiceman.Count());
        }
    }
}
