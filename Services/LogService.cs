
using SystemPanel.Models;

using Microsoft.EntityFrameworkCore;
using SystemPanel.Data;

namespace SystemPanel.Services
{
    public class LogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddLog(LogEntry log)
        {
            _context.LogEntries.Add(log);
            _context.SaveChanges();
        }

        public IEnumerable<LogEntry> GetLogs(string filter = null)
        {
            var query = _context.LogEntries.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(log => log.Message.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }

        public IEnumerable<LogEntry> GetAllLogs()
        {
            return _context.LogEntries.ToList();
        }

        public LogEntry GetLogById(int id)
        {
            return _context.LogEntries.FirstOrDefault(log => log.Id == id);
        }

        public IEnumerable<LogEntry> GetLogsByCriteria(string user, string ip, DateTime timestamp)
        {
            return _context.LogEntries.Where(log =>
                (log.User == user || log.IP == ip) &&
                log.Timestamp.Date == timestamp.Date).ToList();
        }
    }
}
