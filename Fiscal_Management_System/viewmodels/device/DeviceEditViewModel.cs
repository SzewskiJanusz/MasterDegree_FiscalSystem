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
    public class DeviceEditViewModel : AddEditOperationViewModel<Device>
    {
        private StateManager _stateManager;
        public StateManager StateManager { get { return _stateManager; } set { _stateManager = value; } }

        public override void OperateOnDatabase(Device entity)
        {
            using (var ctx = new FiscalDbContext())
            {/*
                Place oldPlace = ctx.Places.FirstOrDefault(x => x.ID == entity.Place.ID);
                
                // Is old place different than new place
                if (!oldPlace.Equals(entity.Place))
                {
                    Place newPlace = ctx.Places.Add(entity.Place);
                    entity.Place = newPlace;
                    oldPlace = ctx.Places.Include("Devices").FirstOrDefault(x => x.ID == entity.Place.ID);
                    // If there are no devices assigned to the old place
                    if (oldPlace.Devices.ToList().Count == 0)
                    {
                        ctx.Places.Remove(oldPlace);
                    }
                }*/

                Place oldPlace = ctx.Places.Include("Devices").FirstOrDefault(x => x.ID == entity.Place.ID);
                // Is old place different than new place
                if (!oldPlace.Equals(entity.Place))
                {
                    Place newPlace = entity.Place;
                    Place NewPlaceExist = ctx.Places.FirstOrDefault(x => x.State == newPlace.State && x.City == newPlace.City && x.Street == newPlace.Street);
                    newPlace.Devices = null;
                    newPlace.ID = 0;

                    if (NewPlaceExist == null)
                    {
                        Place z = ctx.Places.Add(newPlace);
                        entity.Place = z;
                        entity.PlaceId = z.ID;
                    }
                    else
                    {
                        entity.Place = NewPlaceExist;
                        entity.PlaceId = NewPlaceExist.ID;
                        ctx.Entry(ctx.Set<Place>().Find(entity.PlaceId)).CurrentValues.SetValues(entity.Place);
                    }

                    oldPlace = ctx.Places.FirstOrDefault(x => x.ID == oldPlace.ID);

                    if (oldPlace.Devices.Count == 0)
                    {
                        ctx.Places.Remove(oldPlace);
                    }
                }

                ctx.Entry(ctx.Set<Device>().Find(entity.ID)).CurrentValues.SetValues(entity);
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

        public DeviceEditViewModel(Device dev)
        {
            StateManager = new StateManager();

            Entity = dev;

            using (var ctx = new FiscalDbContext())
            {
                Client = ctx.Clients.FirstOrDefault();
            }
            Entity.Client = Client;

            DeviceModel = new DeviceModel();
            DeviceModelManager = new DeviceModelManager();

            ButtonText = "Edytuj";
            WindowTitle = "Edytowanie urządzenia";
        }

        public DeviceEditViewModel(Device d, Client c, Place p)
        {
            StateManager = new StateManager();

            Entity = d;
            Place = p;
            Entity.Place = Place;

            Client = c;

            Entity.Client = Client;

            DeviceModel = new DeviceModel();
            DeviceModelManager = new DeviceModelManager();

            ButtonText = "Edytuj";
            WindowTitle = "Edytowanie urządzenia";
        }
    }
}
