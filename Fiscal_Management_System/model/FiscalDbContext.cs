using Fiscal_Management_System.model.repairgroup;
using Fiscal_Management_System.model.service;

namespace Fiscal_Management_System.model
{
    using Fiscal_Management_System.model.client;
    using Fiscal_Management_System.model.device;
    using Fiscal_Management_System.model.devicemodel;
    using Fiscal_Management_System.model.place;
    using Fiscal_Management_System.model.revenue;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class FiscalDbContext : DbContext, IDbContext
    {
        // Your context has been configured to use a 'Model' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Fiscal_Management_System.model.Model' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model' 
        // connection string in the application configuration file.
        public FiscalDbContext()
            : base("name=FiscalDbContext")
        {
        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Revenue> Revenues {get; set;}
        public virtual DbSet<Device> Devices {get; set;}
        public virtual DbSet<DeviceModel> DeviceModels {get; set;}
        public virtual DbSet<Place> Places {get; set;}
        public virtual DbSet<Service> Services {get; set;}
        public virtual DbSet<TypeOfService> TypeOfServices {get; set;}
        public virtual DbSet<Serviceman> Servicemen {get; set;}
        public virtual DbSet<RepairGroup> RepairGroups {get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        IDbSet<T> IDbContext.Set<T>()
        {
            return this.Set<T>();
        }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}