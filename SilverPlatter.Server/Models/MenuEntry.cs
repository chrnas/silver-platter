namespace SilverPlatter.Server.Models
{
    public class MenuEntry
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int RestaurantId { get; set; }
    }
}
