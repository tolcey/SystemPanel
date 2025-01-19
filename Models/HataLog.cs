
namespace SystemPanel.Models
{
    public class HataLog
    {
        public int LogId { get; set; }
        public int? Kullanici { get; set; }
        public DateTime? Tarih { get; set; }
        public string? HataDetay { get; set; }
        public string? Birimi { get; set; }
        public string? Sayfa { get; set; }
        public string? Method { get; set; }
        public string? Ip { get; set; }
        public string? OlayTuru { get; set; }
    }
}
