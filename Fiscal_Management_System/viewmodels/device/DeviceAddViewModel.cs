using Fiscal_Management_System.model;
using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.devicemodel;
using Fiscal_Management_System.model.place;
using Fiscal_Management_System.viewmodels.client;
using Fiscal_Management_System.viewmodels.devicemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.viewmodels.device
{
    public class DeviceAddViewModel : AddEditOperationViewModel<Device>
    {
        private StateManager _stateManager;
        public StateManager StateManager { get { return _stateManager; } set { _stateManager = value; } }

        public override void OperateOnDatabase(Device entity)
        {
            using (var ctx = new FiscalDbContext())
            {
                entity.DateOfLastService = entity.DateOfInitialization;
                entity.TypeOfLastService = "Fiskalizacja";
                entity.PlannedDateOfNextInspection = DateTime.Now.AddMonths(entity.InspectionPeriodicTimeInMonths);
                DeviceModel dModel = ctx.DeviceModels.FirstOrDefault(x => x.ID == entity.Model.ID);
                Client client = ctx.Clients.FirstOrDefault(x => x.ID == entity.Client.ID);
                entity.Client = client;
                dModel.Amount++;
                entity.Model = dModel;
                Place place = ctx.Places.FirstOrDefault(x => x.ID == entity.Place.ID);

                Device dev = new Device(entity);
                if (place != null)
                    dev.Place = place;
                ctx.Devices.Add(dev);
                ctx.SaveChanges();
            }
        }

        private DeviceModel _deviceModel;
        public DeviceModel DeviceModel
        {
            get { return _deviceModel; }
            set { _deviceModel = value; }
        }

        private Place _place;
        public Place Place
        {
            get { return _place; }
            set { _place = value; }
        }

        private Client _client;
        public Client Client
        {
            get { return _client; }
            set { _client = value; }
        }

        private DeviceModelManager _deviceModelManager;
        public DeviceModelManager DeviceModelManager
        {
            get { return _deviceModelManager; }
            set { _deviceModelManager = value; }
        }

        public DeviceAddViewModel(Client c)
        {
            StateManager = new StateManager();

            Entity = new Device() { InspectionPeriodicTimeInMonths = 24 };
            Place = new Place() { State = "zachodniopomorskie" };
            Entity.Place = Place;

            Client = c;

            Entity.Client = Client;
            Entity.DateOfInitialization = DateTime.Today;

            DeviceModel = new DeviceModel();
            DeviceModelManager = new DeviceModelManager();
            
            ButtonText = "Dodaj";
            WindowTitle = "Dodawanie urządzenia";
        }

        public DeviceAddViewModel(Client c, Place p)
        {
            StateManager = new StateManager();

            Entity = new Device() { InspectionPeriodicTimeInMonths = 24 };
            Place = p;
            Entity.Place = Place;

            Client = c;

            Entity.Client = Client;
            Entity.DateOfInitialization = DateTime.Today;

            DeviceModel = new DeviceModel();
            DeviceModelManager = new DeviceModelManager();

            ButtonText = "Dodaj";
            WindowTitle = "Dodawanie urządzenia";
        }
    }
}
