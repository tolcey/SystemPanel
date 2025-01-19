using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using SystemPanel.Services;
using SystemPanel.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace SystemPanel.Controllers
{
    [Route("Duyurular")]
    public class DuyurularController : BaseController
    {
        private readonly IDuyuruService _service;

        // Modify the constructor to pass the logger to the base class
        public DuyurularController(IDuyuruService service, ILogger<DuyurularController> logger)
            : base(logger) // Passing the logger to the BaseController constructor
        {
            _service = service;
        }

        // Duyuru Listesi
        [HttpGet("List")]
        public async Task<IActionResult> List(int? page, string searchText, string typeFilter)
        {
            int pageSize = 12; // Sayfa başına duyuru sayısı
            int pageNumber = page ?? 1;

            var duyurular = await _service.GetAllAsync();

            // Arama ve filtreleme
            if (!string.IsNullOrEmpty(searchText))
            {
                duyurular = duyurular.Where(d => d.Turu.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                                                 d.Birimi.Contains(searchText, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(typeFilter))
            {
                duyurular = duyurular.Where(d => d.Turu == typeFilter);
            }

            // İstatistik verileri
            ViewBag.TotalCount = duyurular.Count();
            ViewBag.RecentAnnouncement = duyurular.OrderByDescending(d => d.KayitTarihi).FirstOrDefault();
            ViewBag.TodayCount = duyurular.Count(d => d.KayitTarihi?.Date == DateTime.UtcNow.Date);

            var pagedList = duyurular.ToPagedList(pageNumber, pageSize);
            return View(pagedList);
        }

        // Yeni Duyuru Ekle
        [HttpGet("Create")]
        public IActionResult Create() => View();

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Duyuru model)
        {
            if (ModelState.IsValid)
            {
                model.KayitTarihi = DateTime.UtcNow;
                await _service.AddAsync(model);
                TempData["Message"] = "Duyuru başarıyla eklendi.";
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        // Duyuru Detayları
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var duyuru = await _service.GetByIdAsync(id);
            if (duyuru == null)
            {
                TempData["Error"] = "Duyuru bulunamadı.";
                return RedirectToAction(nameof(List));
            }

            return View(duyuru);
        }

        // Duyuru Güncelle
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var duyuru = await _service.GetByIdAsync(id);
            if (duyuru == null)
            {
                TempData["Error"] = "Duyuru bulunamadı.";
                return RedirectToAction(nameof(List));
            }
            return View(duyuru);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Duyuru model)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(id, model);
                TempData["Message"] = "Duyuru başarıyla güncellendi.";
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        // Duyuru Sil
        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var duyuru = await _service.GetByIdAsync(id);
            if (duyuru == null)
            {
                TempData["Error"] = "Duyuru bulunamadı.";
                return RedirectToAction(nameof(List));
            }

            await _service.DeleteAsync(id);
            TempData["Message"] = "Duyuru başarıyla silindi.";
            return RedirectToAction(nameof(List));
        }

        // Seçilenleri Sil
        [HttpPost("DeleteSelected")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSelected([FromForm] int[] selectedIds)
        {
            if (selectedIds == null || selectedIds.Length == 0)
            {
                TempData["Error"] = "Hiçbir duyuru seçilmedi.";
                return RedirectToAction(nameof(List));
            }

            foreach (var id in selectedIds)
            {
                await _service.DeleteAsync(id);
            }

            TempData["Message"] = $"{selectedIds.Length} duyuru başarıyla silindi.";
            return RedirectToAction(nameof(List));
        }
    }
}
