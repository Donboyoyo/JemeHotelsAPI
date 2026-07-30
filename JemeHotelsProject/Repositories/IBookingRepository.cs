using JemeHotelsProject.Models.Domain;

namespace JemeHotelsProject.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> CreateBookingAsync(Booking booking);
        Task<List<Booking>> GetAllBookingAsync();
        Task<Booking?> DeleteBookingAsync(Guid id);
    }
}
