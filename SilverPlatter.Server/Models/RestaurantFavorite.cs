namespace SilverPlatter.Server.Models
{
    public class RestaurantFavorite
    {
        public int Id { get; set; }               
        public int UserId { get; set; }           
        public int RestaurantId { get; set; }     
    }
}