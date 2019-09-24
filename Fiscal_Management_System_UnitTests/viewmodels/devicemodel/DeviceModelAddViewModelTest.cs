using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.devicemodel;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.viewmodels.devicemodel;
using Fiscal_Management_System.viewmodels.revenue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.devicemodel
{
    [TestClass]
    public class DeviceModelAddViewModelTest
    {
        [TestMethod]
        public void AddDeviceModel_Test()
        {
            DeviceModel devModel = new DeviceModel()
            {
                ID = 1,
                Name = "TestModel"
            };

            var mockContext = new Mock<IDbContext>();
            var mockSet = new Mock<IDbSet<DeviceModel>>();

            mockContext.Setup(m => m.Set<DeviceModel>()).Returns(mockSet.Object);

            DeviceModelAddViewModel cavm = new DeviceModelAddViewModel(mockContext.Object);
            cavm.Operation(devModel);

            mockSet.Verify(m => m.Add(It.IsAny<DeviceModel>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
