using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SystemPanel.Data;
using SystemPanel.Models;

namespace SystemPanel.Controllers
{
    public class SccmLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SccmLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SccmLogs
        public IActionResult Index()
        {
            var logs = _context.SccmLogs.ToList();
            return View(logs);
        }

        // GET: SccmLogs/Details/5
        public IActionResult Details(int id)
        {
            var log = _context.SccmLogs.FirstOrDefault(l => l.Id == id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // GET: SccmLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SccmLogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SccmLog log)
        {
            if (ModelState.IsValid)
            {
                _context.SccmLogs.Add(log);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(log);
        }

        // GET: SccmLogs/Edit/5
        public IActionResult Edit(int id)
        {
            var log = _context.SccmLogs.FirstOrDefault(l => l.Id == id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // POST: SccmLogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SccmLog log)
        {
            if (id != log.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(log);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(log);
        }

        // GET: SccmLogs/Delete/5
        public IActionResult Delete(int id)
        {
            var log = _context.SccmLogs.FirstOrDefault(l => l.Id == id);
            if (log == null)
            {
                return NotFound();
            }
            return View(log);
        }

        // POST: SccmLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var log = _context.SccmLogs.FirstOrDefault(l => l.Id == id);
            if (log != null)
            {
                _context.SccmLogs.Remove(log);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // Custom Filter Method
        [HttpGet]
        public IActionResult Filter(string ip)
        {
            var filteredLogs = _context.SccmLogs.Where(l => l.Ip == ip).ToList();
            return View("Index", filteredLogs);
        }
    }
}
