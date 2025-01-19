using Microsoft.AspNetCore.Mvc;
using SystemPanel.Services;  // FileManagementService için doğru namespace


namespace SystemPanel.Controllers
{
    public class FileManagementController : Controller
    {
        private readonly FileManagementService _fileService;
        private readonly ILogger<FileManagementController> _logger;

        public FileManagementController(FileManagementService fileService, ILogger<FileManagementController> logger)
        {
            _fileService = fileService;
            _logger = logger;
        }

        // Dosyaların listelendiği ve gösterildiği metod
        public IActionResult Index()
        {
            try
            {
                var files = _fileService.GetAllFiles();
                return View(files);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching files.");
                TempData["ErrorMessage"] = "Dosyalar alınırken bir hata oluştu.";
                return View();
            }
        }

        // Dosya yükleme işlemi
        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    var result = _fileService.UploadFile(file);
                    if (result)
                    {
                        _logger.LogInformation("File {FileName} uploaded successfully.", file.FileName);
                        TempData["SuccessMessage"] = "Dosya başarıyla yüklendi.";
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Dosya yüklenemedi.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error uploading file {FileName}.", file.FileName);
                    ModelState.AddModelError("", ex.Message);
                }
            }

            TempData["ErrorMessage"] = "Dosya yüklenirken bir hata oluştu.";
            return View("Index");
        }

        // Dosya silme işlemi
        [HttpPost]
        public IActionResult DeleteFile(string fileName)
        {
            try
            {
                var result = _fileService.DeleteFile(fileName);
                if (result)
                {
                    _logger.LogInformation("File {FileName} deleted successfully.", fileName);
                    TempData["SuccessMessage"] = "Dosya başarıyla silindi.";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Dosya silinemedi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file {FileName}.", fileName);
                ModelState.AddModelError("", ex.Message);
            }

            TempData["ErrorMessage"] = "Dosya silinirken bir hata oluştu.";
            return View("Index");
        }
    }
}
