using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Fiscal_Management_System.model.repairgroup;

namespace Fiscal_Management_System.model
{
    [Table("Serwisant")]
    public class Serviceman
    {
        [Column("serwisant_id")]
        public int ID { get; set; }

        [Column("serwisant_imie")]
        public string Name { get; set; }

        [Column("serwisant_nazwisko")]
        public string Surname { get; set; }

        [Column("serwisant_hash")]
        public string Hash { get; set; }

        [Column("serwisant_salt")]
        public string Salt { get; set; }

        [Column("serwisant_telefon")]
        public string Phone { get; set; }

        [Column("serwisant_email")]
        public string Email { get; set; }

        [Column("serwisant_czyglowny")]
        public bool IsMain { get; set; }

        [NotMapped]
        public string NameAndSurname { get { return Name + " " + Surname; } }

        // 0:n on RepairGroup relation
        public ICollection<RepairGroup> Groups { get; set; }
    }
}
