namespace SilverPlatter.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<int>? RestaurantFavorites { get; set; }
        public int Budget { get; set; }
        public List<string>? Allergies { get; set; }
        public int PreferedRating { get; set; }
    }
}