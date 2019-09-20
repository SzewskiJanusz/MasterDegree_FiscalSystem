using System;
using System.Linq;
using Castle.Components.DictionaryAdapter.Xml;
using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.viewmodels.device;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fiscal_Management_System_UnitTests.viewmodels.device
{
    [TestClass]
    public class DeviceViewModelTest
    {
        [TestMethod]
        public void GetAllDevices_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Device>())
                .Returns(new FakeDbSet<Device>
                {
                    new Device() { UniqueNumber = "1111111111" }
                });
            // Act
            DeviceViewModel dvm = new DeviceViewModel(mock.Object);

            
            var allDevices = dvm.GetDataFromDb();

            // Assert
            Assert.AreEqual(1, allDevices.Count());
        }

        [TestMethod]
        public void GetDevicesOfClient_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Device>())
                .Returns(new FakeDbSet<Device>
                {
                    new Device() { UniqueNumber = "1111111111", Client = new Client(){ID = 1}, ClientId = 1},
                    new Device() { UniqueNumber = "2222222222", Client = new Client(){ID = 2}, ClientId = 2},
                    new Device() { UniqueNumber = "3333333333", Client = new Client(){ID = 1}, ClientId = 1}
                });

            DeviceViewModel dvm = new DeviceViewModel(mock.Object);

            // Act
            Client client = new Client() { ID = 1 };
            var allDevices = dvm.GetDataFromDb(client);

            // Assert
            Assert.AreEqual(2, allDevices.Count());
        }

        [TestMethod]
        public void GetDevicesOfClientInPlace_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Device>())
                .Returns(new FakeDbSet<Device>
                {
                    new Device()
                    {
                        UniqueNumber = "1111111111", Client = new Client() {ID = 1}, ClientId = 1,
                        Place = new Place() {ID = 1, State = "Test1", City = "Test1", Street = "Test1"}, PlaceId = 1
                    },
                    new Device()
                    {
                        UniqueNumber = "2222222222", Client = new Client() {ID = 2}, ClientId = 2,
                        Place = new Place() {ID = 2, State = "Test2", City = "Test2", Street = "Test2"}, PlaceId = 2
                    },
                    new Device()
                    {
                        UniqueNumber = "33333333333", Client = new Client() {ID = 2}, ClientId = 2,
                        Place = new Place() {ID = 2, State = "Test2", City = "Test2", Street = "Test2"}, PlaceId = 2
                    },
                    new Device()
                    {
                        UniqueNumber = "4444444444", Client = new Client() {ID = 1}, ClientId = 1,
                        Place =  new Place() {ID = 1, State = "Test1", City = "Test1", Street = "Test1"}, PlaceId = 1
                    }
                });

            mock.Setup(x => x.Set<Place>()).Returns
            (
                new FakeDbSet<Place>()
                {
                    new Place() {ID = 1, State = "Test1", City = "Test1", Street = "Test1"},
                    new Place() {ID = 2, State = "Test2", City = "Test2", Street = "Test2"},
                }
            );

            DeviceViewModel dvm = new DeviceViewModel(mock.Object);

            // Act
            Client client = new Client() { ID = 1 };
            Place place = new Place() {ID = 1, State = "Test1", City = "Test1", Street = "Test1"};
            var allDevices = dvm.GetDataFromDb(client, place);

            // Assert
            Assert.AreEqual(2, allDevices.Count());
        }

        [TestMethod]
        public void GetAllDevicesWithinLiquidated_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Device>())
                .Returns(new FakeDbSet<Device>
                {
                    new Device() { UniqueNumber = "1111111111" },
                    new Device() { UniqueNumber = "3233233323", DateOfLiquidation = new DateTime()}

                });
            DeviceViewModel dvm = new DeviceViewModel(mock.Object);
            // Act


            var allDevices = dvm.GetAllDevicesFromDb();

            // Assert
            Assert.AreEqual(2, allDevices.Count());
        }

        [TestMethod]
        public void GetDevicesOfClientWithinLiquidated_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Device>())
                .Returns(new FakeDbSet<Device>
                {
                    new Device() { UniqueNumber = "1111111111", Client = new Client(){ID = 1}, ClientId = 1},
                    new Device() { UniqueNumber = "2222222222", Client = new Client(){ID = 2}, ClientId = 2, DateOfLiquidation = new DateTime()},
                    new Device() { UniqueNumber = "3333333333", Client = new Client(){ID = 1}, ClientId = 1},
                    new Device() { UniqueNumber = "3332345333", Client = new Client(){ID = 1}, ClientId = 1, DateOfLiquidation = new DateTime()}
                });

            DeviceViewModel dvm = new DeviceViewModel(mock.Object);

            // Act
            Client client = new Client() { ID = 1 };
            var allDevices = dvm.GetAllDevicesFromDb(client);


            // Assert
            Assert.AreEqual(3, allDevices.Count());
        }

        [TestMethod]
        public void GetDevicesOfClientInPlaceWithinLiquidated_Test()
        {
            //Arrange
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Device>())
                .Returns(new FakeDbSet<Device>
                {
                    new Device()
                    {
                        UniqueNumber = "1111111111", Client = new Client() {ID = 1}, ClientId = 1,
                        Place = new Place() {ID = 1, State = "Test1", City = "Test1", Street = "Test1"}, PlaceId = 1, DateOfLiquidation = new DateTime()
                    },
                    new Device()
                    {
                        UniqueNumber = "2222222222", Client = new Client() {ID = 2}, ClientId = 2,
                        Place = new Place() {ID = 2, State = "Test2", City = "Test2", Street = "Test2"}, PlaceId = 2
                    },
                    new Device()
                    {
                        UniqueNumber = "33333333333", Client = new Client() {ID = 2}, ClientId = 2,
                        Place = new Place() {ID = 2, State = "Test2", City = "Test2", Street = "Test2"}, PlaceId = 2, DateOfLiquidation = new DateTime()
                    },
                    new Device()
                    {
                        UniqueNumber = "4444444444", Client = new Client() {ID = 1}, ClientId = 1,
                        Place =  new Place() {ID = 1, State = "Test1", City = "Test1", Street = "Test1"}, PlaceId = 1
                    }
                });

            mock.Setup(x => x.Set<Place>()).Returns
            (
                new FakeDbSet<Place>()
                {
                    new Place() {ID = 1, State = "Test1", City = "Test1", Street = "Test1"},
                    new Place() {ID = 2, State = "Test2", City = "Test2", Street = "Test2"},
                }
            );

            DeviceViewModel dvm = new DeviceViewModel(mock.Object);

            // Act
            Client client = new Client() { ID = 1 };
            Place place = new Place() { ID = 1, State = "Test1", City = "Test1", Street = "Test1" };
            var allDevices = dvm.GetAllDevicesFromDb(client, place);

            // Assert
            Assert.AreEqual(2, allDevices.Count());
        }
    }
}
