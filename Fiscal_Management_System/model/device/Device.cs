using Fiscal_Management_System.model.client;
using Fiscal_Management_System.model.devicemodel;
using Fiscal_Management_System.model.place;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiscal_Management_System.model.device
{
    [Table("Urzadzenie")]
    public class Device : INotifyPropertyChanged
    {
        [Column("urzadzenie_id")]
        public int ID { get; set; }

        private string _uniqueNumber;
        [Column("urzadzenie_nr_unikatowy")]
        public string UniqueNumber
        {
            get { return _uniqueNumber; }
            set { _uniqueNumber = value; OnPropertyChanged("UniqueNumber"); }
        }

        private string _registerNumber;
        [Column("urzadzenie_nr_ewidencyjny")]
        public string RegisterNumber
        {
            get { return _registerNumber; }
            set { _registerNumber = value; OnPropertyChanged("RegisterNumber"); }
        }

        private string _serialNumber;
        [Column("urzadzenie_nr_fabryczny")]
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { _serialNumber = value; OnPropertyChanged("SerialNumber"); }
        }

        private DateTime _dateOfInitialization;
        [Column("urzadzenie_data_fiskalizacji")]
        public DateTime DateOfInitialization
        {
            get { return _dateOfInitialization; }
            set { _dateOfInitialization = value; OnPropertyChanged("DateOfInitialization"); }
        }

        private DateTime _dateOfLastService;
        [Column("urzadzenie_data_ostatniej_uslugi")]
        public DateTime DateOfLastService
        {
            get { return _dateOfLastService; }
            set { _dateOfLastService = value; OnPropertyChanged("DateOfLastService"); }
        }

        private string _typeOfLastService;
        [Column("urzadzenie_nazwa_ostatniej_uslugi")]
        public string TypeOfLastService
        {
            get { return _typeOfLastService; }
            set { _typeOfLastService = value; OnPropertyChanged("TypeOfLastService"); }
        }

        private DateTime? _dateOfLastInspection;
        [Column("urzadzenie_data_ostatniego_przegladu")]
        public DateTime? DateOfLastInspection
        {
            get { return _dateOfLastInspection; }
            set { _dateOfLastInspection = value; OnPropertyChanged("DateOfLastInspection"); }
        }

        private DateTime _plannedDateOfNextInspection;
        [Column("urzadzenie_data_zaplanowanego_przegladu")]
        public DateTime PlannedDateOfNextInspection
        {
            get { return _plannedDateOfNextInspection; }
            set { _plannedDateOfNextInspection = value; OnPropertyChanged("PlannedDateOfNextInspection"); }
        }

        private DateTime? _dateOfLiquidation;
        [Column("urzadzenie_data_likwidacji")]
        public DateTime? DateOfLiquidation
        {
            get { return _dateOfLiquidation; }
            set { _dateOfLiquidation = value; OnPropertyChanged("DateOfLiquidation"); }
        }

        private int _inspectionPeriodicTime;
        [Column("urzadzenie_co_ile_miesiecy_przeglad")]
        public int InspectionPeriodicTimeInMonths
        {
            get { return _inspectionPeriodicTime; }
            set { _inspectionPeriodicTime = value; OnPropertyChanged("InspectionPeriodicTimeInMonths"); }
        }

        [Column("kontrahent_id")]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        [Column("model_id")]
        public int ModelId { get; set; }
        [ForeignKey("ModelId")]
        public DeviceModel Model { get; set; }

        [Column("miejsce_id")]
        public int PlaceId { get; set; }
        [ForeignKey("PlaceId")]
        public Place Place { get; set; }

        /// <summary>
        /// Copy contructor
        /// </summary>
        /// <param name="c"></param>
        public Device(Device c)
        {
            this.ID = c.ID;
            this.UniqueNumber = c.UniqueNumber;
            this.RegisterNumber = c.RegisterNumber;
            this.SerialNumber = c.SerialNumber;
            this.DateOfInitialization = c.DateOfInitialization;
            this.DateOfLastService = c.DateOfLastService;
            this.TypeOfLastService = c.TypeOfLastService;
            this.DateOfLastInspection = c.DateOfLastInspection;
            this.PlannedDateOfNextInspection = c.PlannedDateOfNextInspection;
            this.DateOfLiquidation = c.DateOfLiquidation;
            this.InspectionPeriodicTimeInMonths = c.InspectionPeriodicTimeInMonths;
            this.Client = c.Client;
            this.ClientId = c.ClientId == 0 ? c.Client.ID : c.ClientId;
            this.Model = c.Model;
            this.ModelId = c.ModelId == 0 ? c.Model.ID : c.ModelId;
            this.Place = c.Place;
            this.PlaceId = c.PlaceId == 0 ? c.Model.ID : c.ModelId;
        }

        public Device() { }

        #region INotifyPropertyChanged things
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
