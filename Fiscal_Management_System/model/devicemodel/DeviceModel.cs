﻿using Fiscal_Management_System.model.device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.model.devicemodel
{
    [Table("ModelUrzadzenia")]
    public class DeviceModel : INotifyPropertyChanged
    {
        [Column("model_id")]
        public int ID { get; set; }

        private string _name;
        [Column("model_nazwa")]
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        // 0:n on Device relation
        public ICollection<Device> Devices { get; set; }

        public DeviceModel()
        {
            Devices = new HashSet<Device>();
        }

        public DeviceModel(DeviceModel dm)
        {
            ID = dm.ID;
            Name = dm.Name;
            Devices = dm.Devices;
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
