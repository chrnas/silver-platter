using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public interface IAllergyRepository
    {
        List<Allergy> GetAll();
        List<Allergy> GetByUserId(int userId);
        Allergy? GetById(int id);
        Allergy Add(Allergy allergy);
        Allergy Update(Allergy allergy);
        bool RemoveById(int id);
    }
}
