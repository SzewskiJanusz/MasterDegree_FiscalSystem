using System.ComponentModel.DataAnnotations.Schema;

namespace Fiscal_Management_System.model
{
    [Table("Kontrahent")]
    public class Kontrahent
    {
        [Column("kontrahent_id")]
        public int ID { get; set; }
        [Column("kontrahent_nazwa")]
        public string Name { get; set; }
        [Column("kontrahent_symbol")]
        public string Symbol { get; set; }
        [Column("kontrahent_nip")]
        public string NIP { get; set; }
        [Column("kontrahent_wojewodztwo")]
        public string State { get; set; }
        [Column("kontrahent_miasto")]
        public string City { get; set; }
        [Column("kontrahent_ulica")]
        public string Street { get; set; }
        [Column("kontrahent_kod_pocztowy")]
        public string PostalCode { get; set; }
        [Column("kontrahent_telefon")]
        public string Phone { get; set; }
        [Column("kontrahent_email")]
        public string Email { get; set; }
    }
}
