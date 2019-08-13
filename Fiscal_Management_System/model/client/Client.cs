using System.ComponentModel;
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

        #region INotifyPropertyChanged things
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
