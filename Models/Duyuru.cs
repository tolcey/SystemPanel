namespace SystemPanel.Models
{
    public class Duyuru
    {
        public int Id { get; set; }
        public string? Turu { get; set; }         // Duyurunun t�r� (�rne�in, bilgilendirme, uyar�)
        public string? Birimi { get; set; }      // �lgili birim
        public DateTime? KayitTarihi { get; set; } // Kay�t tarihi
        public string? Detay { get; set; }       // Duyurunun detay i�eri�i
        public int? Yazan { get; set; }          // Duyuruyu olu�turan kullan�c�
        public string? Resim1 { get; set; }      // Resim URL'leri veya dosya yollar�
        public string? Resim2 { get; set; }
        public string? Resim3 { get; set; }
        public string? Resim4 { get; set; }
    }
}
