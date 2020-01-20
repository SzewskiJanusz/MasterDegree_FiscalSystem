using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.devicemodel;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.model.service;
using Fiscal_Management_System.viewmodels.client;
using Fiscal_Management_System.viewmodels.device;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.device
{
    [TestClass]
    public class DeviceAddViewModelTest
    {
        [TestMethod]
        public void AdditionOfDeviceTest()
        {
            Client c = new Client() { ID = 1 };
            Place p = new Place() { ID = 1 };
            DeviceModel dm = new DeviceModel() {ID= 1, Name = "Test"};
            Device device = new Device()
            {
                UniqueNumber = "TestNumber",
                Client = c,
                Place = p,
                Model = dm,
                InspectionPeriodicTimeInMonths = 24
            };

            var mockContext = new Mock<IDbContext>();
            var mockSet = new Mock<DbSet<Device>>();
            var mockSetServices = new Mock<DbSet<Service>>();

            mockContext.Setup(m => m.Set<Device>()).Returns(mockSet.Object);

            mockContext.Setup(m => m.Set<Place>()).
                Returns(new FakeDbSet<Place>(){ new Place() { ID = 1}});
            
            mockContext.Setup(m => m.Set<Client>()).
                Returns(new FakeDbSet<Client>() { new Client() { ID = 1 } });

            mockContext.Setup(m => m.Set<DeviceModel>()).
                Returns(new FakeDbSet<DeviceModel>() { new DeviceModel() { ID= 1, Name="Test" } });

            mockContext.Setup(m => m.Set<TypeOfService>()).
                Returns(new FakeDbSet<TypeOfService>() { new TypeOfService() { Id = 1, Name = "Przegląd kasy fiskalnej" } });

            mockContext.Setup(m => m.Set<Service>()).Returns(mockSetServices.Object);
            DeviceAddViewModel cavm = new DeviceAddViewModel(mockContext.Object, c, p);
            cavm.Operation(device);

            mockSetServices.Verify(m => m.Add(It.IsAny<Service>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
