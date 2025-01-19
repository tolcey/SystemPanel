namespace SystemPanel.Models
{
    public class SMSRequest
    {
        public required string PhoneNumber { get; set; }
        public required string Message { get; set; }
    }
}
