namespace SystemPanel.Models
{ 
public class ServiceStatus
{
    public int Id { get; set; }                // Servis için benzersiz kimlik
    public required string ServiceName { get; set; }    // Servis adı (ör. "CcmExec")
    public required string State { get; set; }          // Servis durumu (ör. "Running", "Stopped")
    public required string Status { get; set; }         // Genel durum (ör. "OK", "Warning")
}
}