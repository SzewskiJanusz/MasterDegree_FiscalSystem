using Fiscal_Management_System.model.client;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiscal_Management_System.model.revenue
{
    [Table("UrzadSkarbowy")]
    public class Revenue
    {
        [Column("urzad_id")]
        public int ID { get; set; }
        [Column("urzad_nazwa")]
        public string Name { get; set; }
        [Column("urzad_miasto")]
        public string City { get; set; }
        [Column("urzad_ulica")]
        public string Street { get; set; }

       // [ForeignKey("RevenueId")]
        public ICollection<Client> Clients { get; set; }

        public Revenue()
        {
            Clients = new HashSet<Client>();
        }
    }
}
