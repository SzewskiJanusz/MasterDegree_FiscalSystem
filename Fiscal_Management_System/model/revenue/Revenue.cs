using System;
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

        public ICollection<Client> Clients { get; set; }

        [NotMapped]
        public string Address => string.Concat(City, ", ", Street);

        public Revenue()
        {
            Clients = new HashSet<Client>();
        }

        public Revenue(Revenue r)
        {
            ID = r.ID;
            Name = r.Name;
            City = r.City;
            Street = r.Street;
            Clients = r.Clients;
        }
    }
}
