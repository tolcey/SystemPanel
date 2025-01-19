
namespace SystemPanel.Models
{
    public class Havale
    {
        public int HavaleId { get; set; }
        public int? TalepId { get; set; }
        public int? HavaleEden { get; set; }
        public DateTime? HavaleTarihi { get; set; }
        public string? HavaleNot { get; set; }
        public int? HavaleEdilen { get; set; }
        public int? Onay { get; set; }
        public string? OnayNotu { get; set; }
        public DateTime? OnayTarihi { get; set; }
        public int? Onaylayan { get; set; }
    }
}
