using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Fiscal_Management_System.model.device;

namespace Fiscal_Management_System.model.service
{
    [Table("Usługa")]
    public class Service : INotifyPropertyChanged
    {
        [Column("usluga_id")]
        public int Id { get; set; }

        [Column("usluga_data_przyjecia")]
        public DateTime ApproveTime { get; set; }

        [Column("usluga_data_wykonania")]
        public DateTime? ExecutionTime { get; set; }

        [Column("usluga_cena")]
        public Decimal ChargedPrice { get; set; }

        [Column("usluga_planowana_data_wykonania")]
        public DateTime PlannedDateOfExecution { get; set; }

        // FK
        //  Device
        [Column("urzadzenie_id")]
        public int DeviceId { get; set; }

        [ForeignKey("DeviceId")]
        public Device Device { get; set; }

        //  Type
        [Column("typ_id")]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public TypeOfService TypeOfService { get; set; }

        public Service() { }

        public Service(Service s)
        {
            this.Id = s.Id;
            this.ApproveTime = s.ApproveTime;
            this.ExecutionTime = s.ExecutionTime;
            this.ChargedPrice = s.ChargedPrice;
            this.PlannedDateOfExecution = s.PlannedDateOfExecution;
            this.Device = s.Device;
            this.DeviceId = s.DeviceId == 0 ? s.Device.ID : s.DeviceId;
            this.TypeOfService = s.TypeOfService;
            this.TypeId = s.TypeId == 0 ? s.TypeOfService.Id : s.TypeId;
        }

        #region INotifyPropertyChanged things
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
