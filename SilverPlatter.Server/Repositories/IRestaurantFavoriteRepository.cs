using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public interface IRestaurantFavoriteRepository
    {
        List<RestaurantFavorite> GetAll();
        List<RestaurantFavorite> GetByUserId(int userId);
        RestaurantFavorite? GetById(int id);
        RestaurantFavorite Add(RestaurantFavorite favorite);
        bool RemoveById(int id);
        bool RemoveByUserAndRestaurant(int userId, int restaurantId);
    }
}
