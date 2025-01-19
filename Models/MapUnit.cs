namespace SystemPanel.Models
{
    public class MapUnit
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }
    }
}
