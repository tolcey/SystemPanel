using Microsoft.EntityFrameworkCore;
using SystemPanel.Data;
using SystemPanel.Models;

namespace SystemPanel.Services;

public class DuyuruService : IDuyuruService
{
    private readonly ApplicationDbContext _context;

    public DuyuruService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Tüm Duyuruları Tarihe Göre Sıralayıp Getir
    public async Task<IEnumerable<Duyuru>> GetAllAsync()
    {
        return await _context.Duyurular
                             .OrderByDescending(d => d.KayitTarihi) // En yeni duyurular önce gelir
                             .ToListAsync();
    }

    // Belirli Bir Duyuruyu ID ile Getir
    public async Task<Duyuru?> GetByIdAsync(int id)
    {
        return await _context.Duyurular.FindAsync(id);
    }

    // Yeni Duyuru Ekle
    public async Task AddAsync(Duyuru duyuru)
    {
        _context.Duyurular.Add(duyuru);
        await _context.SaveChangesAsync();
    }

    // Mevcut Duyuruyu Güncelle
    public async Task UpdateAsync(int id, Duyuru duyuru)
    {
        var existingDuyuru = await _context.Duyurular.FindAsync(id);
        if (existingDuyuru != null)
        {
            existingDuyuru.Turu = duyuru.Turu;
            existingDuyuru.Birimi = duyuru.Birimi;
            existingDuyuru.Detay = duyuru.Detay;
            existingDuyuru.KayitTarihi = duyuru.KayitTarihi;
            await _context.SaveChangesAsync();
        }
    }

    // Duyuruyu Sil
    public async Task DeleteAsync(int id)
    {
        var duyuru = await _context.Duyurular.FindAsync(id);
        if (duyuru != null)
        {
            _context.Duyurular.Remove(duyuru);
            await _context.SaveChangesAsync();
        }
    }
}
