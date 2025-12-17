using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public interface IBookingTableRepository
    {
        List<BookingTable> GetAll();
        BookingTable? GetById(int id);
        List<BookingTable> GetByRestaurantId(int id);
        BookingTable Add(BookingTable table);
        BookingTable Update(BookingTable table);
        bool RemoveById(int id);
    }
}
