using SystemPanel.Models;

namespace SystemPanel.ViewModels.Logs
{
    public class RelatedLogsViewModel
    {
        public LogEntry CurrentLog { get; set; }
        public IEnumerable<LogEntry> RelatedLogs { get; set; }
    }
}
