using System.Reflection.Metadata;
using SilverPlatter.Server.Models;
namespace SilverPlatter.Server.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetById(int id);
        User Add(User user);
        User Update(User user);
        bool RemoveById(int id);
    }
}