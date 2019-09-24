using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.devicemodel;
using Fiscal_Management_System.viewmodels.devicemodel;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.devicemodel
{
    [TestClass]
    public class DeviceModelViewModelTest
    {
        [TestMethod]
        public void GetAllDeviceModels_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<DeviceModel>())
                .Returns(new FakeDbSet<DeviceModel>
                {
                    new DeviceModel() { Name = "TestModel"}
                });

            DeviceModelViewModel dvm = new DeviceModelViewModel(null, mock.Object);

            // Act
            var allModels= dvm.GetAllDeviceModels();

            // Assert
            Assert.AreEqual(1, allModels.Count());
        }
    }
}
