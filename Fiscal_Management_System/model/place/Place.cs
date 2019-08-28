using Fiscal_Management_System.model.device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Fiscal_Management_System.model.place
{
    [Table("MiejsceInstalacji")]
    public class Place : INotifyPropertyChanged
    {
        [Column("miejsce_id")]
        public int ID { get; set; }

        private string _state;
        [Column("miejsce_wojewodztwo")]
        public string State
        {
            get { return _state;}
            set { _state = value; OnPropertyChanged("State"); }
        }

        private string _city;
        [Column("miejsce_miasto")]
        public string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged("City"); }
        }

        private string _street;
        [Column("miejsce_ulica")]
        public string Street
        {
            get { return _street; }
            set { _street = value; OnPropertyChanged("Street"); }
        }
        // 0:n on Device relation
        public ICollection<Device> Devices { get; set; }

        #region INotifyPropertyChanged things
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
