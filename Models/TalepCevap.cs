using System.ComponentModel.DataAnnotations;

namespace SystemPanel.Models
{
    public class TalepCevap
    {
        [Key]
        public int CevapId { get; set; } // Birincil anahtar
        public int TalepId { get; set; }
        public string? Cevap { get; set; }
        public int? CevaplayanSicil { get; set; }
        public string? CevaplayanIsim { get; set; }
        public DateTime? CevapTarihi { get; set; }
    }
}
