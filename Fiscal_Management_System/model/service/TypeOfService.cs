using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Fiscal_Management_System.model.service
{
    [Table("TypUslugi")]
    public class TypeOfService : INotifyPropertyChanged
    {
        [Column("typ_id")]
        public int Id { get; set; }
        [Column("typ_nazwa")]
        public string Name { get; set; }
        [Column("typ_cena")]
        public Decimal Price { get; set; }

        public ICollection<Service> Services { get; set; }

        [NotMapped] public int DoneServicesCount => Services.Count(x => x.ExecutionTime.HasValue);

        [NotMapped] public int PlannedServicesCount => Services.Count(x => !x.ExecutionTime.HasValue);

        public TypeOfService() { }

        public TypeOfService(TypeOfService tos)
        {
            Id = tos.Id;
            Name = tos.Name;
            Price = tos.Price;
            Services = tos.Services;
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
