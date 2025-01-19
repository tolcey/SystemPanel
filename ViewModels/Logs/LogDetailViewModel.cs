namespace SystemPanel.ViewModels.Logs
{
    public class LogDetailViewModel
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
