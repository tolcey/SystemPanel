
namespace SystemPanel.Models
{
    public class IslemLog
    {
        public int LogId { get; set; }
        public int? IslemiYapan { get; set; }
        public DateTime? Tarih { get; set; }
        public string? IslemDetay { get; set; }
        public string? Birimi { get; set; }
        public string? Sayfa { get; set; }
        public string? Ip { get; set; }
        public string? IslemTuru { get; set; }
        public string? Sorgulanan { get; set; }
        public string? IslemiYapanBirimi { get; set; }
    }
}
