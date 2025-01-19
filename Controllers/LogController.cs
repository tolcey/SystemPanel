using Microsoft.AspNetCore.Mvc;
using System.Text;
using SystemPanel.Services;
using SystemPanel.ViewModels.Logs;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace SystemPanel.Controllers
{
    public class LogController : Controller
    {
        private readonly LogService _logService;

        public LogController(LogService logService)
        {
            _logService = logService;
        }

        // Log Arama
        public IActionResult Search(string query)
        {
            var results = _logService.GetLogs(query);
            return View("Index", results);  // Index görünümüne sonuçları gönder
        }

        // PDF Export
        public IActionResult ExportToPdf()
        {
            var logs = _logService.GetAllLogs();

            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdfDoc = new PdfDocument(writer);
            var document = new Document(pdfDoc);

            document.Add(new Paragraph("Log Listesi"));
            foreach (var log in logs)
            {
                document.Add(new Paragraph($"[{log.Timestamp}] {log.Level} - {log.Message}"));
            }

            document.Close();

            return File(stream.ToArray(), "application/pdf", "logs.pdf");
        }

        // Logların Listelenmesi
        public IActionResult Index(string filter = "")
        {
            var logs = _logService.GetLogs(filter);
            return View(logs);  // Görünümün doğru olduğundan emin olun
        }

        // CSV Export
        public IActionResult ExportToCsv()
        {
            var logs = _logService.GetAllLogs();

            var csv = new StringBuilder();
            csv.AppendLine("ID,Timestamp,Level,Message");

            foreach (var log in logs)
            {
                csv.AppendLine($"{log.Id},{log.Timestamp},{log.Level},{log.Message}");
            }

            return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "logs.csv");
        }

        // İlgili Loglar
        public IActionResult RelatedLogs(int id)
        {
            var currentLog = _logService.GetLogById(id);
            if (currentLog == null)
            {
                return NotFound();
            }

            var relatedLogs = _logService.GetLogsByCriteria(currentLog.User, currentLog.IP, currentLog.Timestamp);

            var viewModel = new RelatedLogsViewModel
            {
                CurrentLog = currentLog,
                RelatedLogs = relatedLogs
            };

            return View(viewModel);
        }

        // Log Detayları
        public IActionResult Details(int id)
        {
            var log = _logService.GetLogById(id);

            if (log == null)
            {
                return NotFound();
            }

            var viewModel = new LogDetailViewModel
            {
                Id = log.Id,
                Level = log.Level,
                Message = log.Message,
                Timestamp = log.Timestamp
            };

            return View(viewModel);
        }
    }
}
