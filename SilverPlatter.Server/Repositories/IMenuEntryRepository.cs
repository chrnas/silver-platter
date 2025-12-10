using SilverPlatter.Server.Models;
namespace SilverPlatter.Server.Repositories
{
    public interface IMenuEntryRepository
    {
        List<MenuEntry> GetAll();
        MenuEntry? GetById(int id);
        MenuEntry Add(MenuEntry table);
        MenuEntry Update(MenuEntry table);
        bool RemoveById(int id);
    }
}
