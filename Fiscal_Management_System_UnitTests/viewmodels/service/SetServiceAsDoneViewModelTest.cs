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
using Fiscal_Management_System.viewmodels.revenue;
using Fiscal_Management_System.viewmodels.service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.service
{
    [TestClass]
    public class SetServiceAsDoneViewModelTest
    {
        [TestMethod]
        public void SetServiceAsDone_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            var mockSetServices = new Mock<IDbSet<Service>>();

            Client c = new Client() { ID = 1 };
            Place p = new Place() { ID = 1 };
            DeviceModel dm = new DeviceModel() { ID = 1, Name = "Test" };
            Device device = new Device()
            {
                UniqueNumber = "TestNumber",
                Client = c,
                Place = p,
                Model = dm,
                InspectionPeriodicTimeInMonths = 24
            };
            mock.Setup(m => m.Set<TypeOfService>()).
                 Returns(new FakeDbSet<TypeOfService>() { new TypeOfService() { Id = 1, Name = "Przegląd kasy fiskalnej" } });

            mock.Setup(m => m.Set<Service>()).Returns(mockSetServices.Object);

            mock.Setup(x => x.Set<Service>())
                .Returns(new FakeDbSet<Service>
                {
                    new Service() { ExecutionTime = null, PlannedDateOfExecution = new DateTime(), Device = device }
                });

            SetServiceAsDoneViewModel dvm = new SetServiceAsDoneViewModel(null, mock.Object);

            // Act
            dvm.AddDateOfExecutionInService(mock.Object.Set<Service>().FirstOrDefault());

            // Assert
            mock.Verify(m => m.SaveChanges(), Times.Once());
            Assert.IsNotNull(mock.Object.Set<Service>().FirstOrDefault().ExecutionTime);
        }
    }
}
