namespace SilverPlatter.Server.Models
{
    public class BookingTable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Places { get; set; }
        public bool Booked { get; set; }
        public int RestaurantId { get; set; }
    }
}
