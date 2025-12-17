namespace SilverPlatter.Server.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
        public int Budget { get; set; }
        public string? Address { get; set; }
    }
}
