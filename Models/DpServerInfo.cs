namespace SystemPanel.Models
{
    public class DpServerInfo
    {
        public required string ServerName { get; set; }
        public required string IpAddress { get; set; }
        public required List<ServiceStatus> Services { get; set; }
    }
}
