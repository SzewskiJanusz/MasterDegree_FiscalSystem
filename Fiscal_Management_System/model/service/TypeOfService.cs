using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

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

        #region INotifyPropertyChanged things
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
