using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public interface IRestaurantRepository
    {
        List<Restaurant> GetAll();
        Restaurant? GetById(int id);
        Restaurant Add(Restaurant restuarant);
        Restaurant Update(Restaurant restuarant);
        void RemoveById(int id);
    }
}
