using SilverPlatter.Server.Models;

namespace SilverPlatter.Server.Repositories
{
    public interface IBookingTableRepository
    {
        List<BookingTable> GetAll();
        BookingTable? GetById(int id);
        BookingTable Add(BookingTable table);
        BookingTable Update(BookingTable table);
        void RemoveById(int id);
    }
}
