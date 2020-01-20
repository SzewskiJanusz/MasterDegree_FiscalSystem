using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.model.revenue;
using Fiscal_Management_System.viewmodels.place;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace Fiscal_Management_System_UnitTests.viewmodels.place
{
    [TestClass]
    public class PlaceViewModelTest : DbInitForTests
    {
        [TestMethod]
        public void GetClientPlacesFromDb_Test()
        {
            //Arrange
            Client client = new Client()
            {
                ID = 1,
                Name = "TestClient",
                Symbol = "TestSymbol",
                NIP = "3234321234",
                State = "TestState",
                City = "TestCity",
                Street = "TestStreet",
                PostalCode = "11-111",
                Phone = "123-123-123",
                Email = "client@test.com",
                RevenueId = 1,
                Revenue = new Revenue()
                {
                    Name = "TestRevenue",
                    City = "TestCity",
                    Street = "TestStreet"
                },
                Devices =
                { new Device() { UniqueNumber = "1111111111", ClientId = 1, Place = new Place() { ID = 1, City = "TestCity" }, PlaceId = 1 } }
             
            };
            Place p = new Place() { ID = 1, City = "TestCity" };

            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Place>())
                .Returns(new FakeDbSet<Place>
                {
                    p
                });
            mock.Setup(x => x.Set<Device>())
                .Returns(new FakeDbSet<Device>
                {
                    new Device() { UniqueNumber = "1111111111", Client = client, ClientId = 1, Place = p, PlaceId = 1  }
                });
            mock.Setup(x => x.Set<Client>())
                .Returns(new FakeDbSet<Client>
                {
                    client
                });

            // Act
            PlaceViewModel dvm = new PlaceViewModel(client, mock.Object, null);


            var clientPlaces = dvm.GetClientPlacesFromDb(client);

            // Assert
            Assert.AreEqual(1, clientPlaces.Count());
        }

    }
}
