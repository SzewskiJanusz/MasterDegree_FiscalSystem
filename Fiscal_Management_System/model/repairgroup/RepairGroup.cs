using System.ComponentModel.DataAnnotations.Schema;
using Fiscal_Management_System.model.device;

namespace Fiscal_Management_System.model.repairgroup
{
    [Table("GrupaNaprawcza")]
    public class RepairGroup
    {
        [Column("grupa_id")]
        public int ID { get; set; }

        /// <summary>
        /// Name and surname of serviceman
        /// </summary>
        [Column("grupa_ktory_pierwszy")]
        public string FirstServiceman { get; set; }

        [Column("urzadzenie_id")]
        public int DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }

        [Column("serwisant_id")]
        public int ServicemanId { get; set; }
        [ForeignKey("ServicemanId")]
        public Serviceman Serviceman { get; set; }
    }
}
