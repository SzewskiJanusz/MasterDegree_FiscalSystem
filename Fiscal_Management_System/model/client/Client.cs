using Fiscal_Management_System.model.device;
using Fiscal_Management_System.model.revenue;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiscal_Management_System.model.client
{
    [Table("Kontrahent")]
    public class Client : INotifyPropertyChanged
    {
        [Column("kontrahent_id")]
        public int ID { get; set; }

        private string _name;
        [Column("kontrahent_nazwa")]
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private string _symbol;
        [Column("kontrahent_symbol")]
        public string Symbol
        {
            get { return _symbol; }
            set { _symbol = value;  OnPropertyChanged("Symbol"); }
        }

        private string _nip;
        [Column("kontrahent_nip")]
        public string NIP
        {
            get { return _nip; }
            set { _nip = value; OnPropertyChanged("NIP"); }
        }

        private string _state;
        [Column("kontrahent_wojewodztwo")]
        public string State
        {
            get { return _state; }
            set { _state = value; OnPropertyChanged("State"); }
        }

        private string _city;
        [Column("kontrahent_miasto")]
        public string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged("City"); }
        }

        private string _street;
        [Column("kontrahent_ulica")]
        public string Street
        {
            get { return _street; }
            set { _street = value; OnPropertyChanged("Street"); }
        }

        private string _postalCode;
        [Column("kontrahent_kod_pocztowy")]
        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; OnPropertyChanged("PostalCode"); }
        }

        private string _phone;
        [Column("kontrahent_telefon")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged("Phone"); }
        }

        private string _email;
        [Column("kontrahent_email")]
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged("Email"); }
        }

       // [ForeignKey("Revenue")]
        [Column("urzad_id")]
        public int RevenueId { get; set; }
        [ForeignKey("RevenueId")]
        public Revenue Revenue { get; set; }

        // 0:n on Device relation
        public ICollection<Device> Devices { get; set; }

        /// <summary>
        /// Copy contructor
        /// </summary>
        /// <param name="c"></param>
        public Client(Client c)
        {
            this.ID = c.ID;
            this.NIP = c.NIP;
            this.Name = c.Name;
            this.Symbol = c.Symbol;
            this.State = c.State;
            this.City = c.City;
            this.Street = c.Street;
            this.PostalCode = c.PostalCode;
            this.Phone = c.Phone;
            this.Email = c.Email;
            this.RevenueId = c.Revenue.ID;
            this.Revenue = c.Revenue;
            this.Devices = c.Devices;
        }

        public Client()
        {
            Devices = new HashSet<Device>();
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
