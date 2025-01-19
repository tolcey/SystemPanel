namespace SystemPanel.Models
{
    public class StatisticsViewModel
    {
        public required int TotalUsers { get; set; }
        public required int ActiveSessions { get; set; }
        public required string SystemHealth { get; set; }
    }
}