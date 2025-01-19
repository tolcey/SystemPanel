namespace SystemPanel.Models
{
    public class Duyuru
    {
        public int Id { get; set; }
        public string? Turu { get; set; }         // Duyurunun türü (örneðin, bilgilendirme, uyarý)
        public string? Birimi { get; set; }      // Ýlgili birim
        public DateTime? KayitTarihi { get; set; } // Kayýt tarihi
        public string? Detay { get; set; }       // Duyurunun detay içeriði
        public int? Yazan { get; set; }          // Duyuruyu oluþturan kullanýcý
        public string? Resim1 { get; set; }      // Resim URL'leri veya dosya yollarý
        public string? Resim2 { get; set; }
        public string? Resim3 { get; set; }
        public string? Resim4 { get; set; }
    }
}
