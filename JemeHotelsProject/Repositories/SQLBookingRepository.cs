using JemeHotelsProject.Data;
using JemeHotelsProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JemeHotelsProject.Repositories
{
    public class SQLBookingRepository : IBookingRepository
    {
        private readonly JemeHotelsDbContext dbContext;

        public SQLBookingRepository(JemeHotelsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            await dbContext.Bookings.AddAsync(booking);
            await dbContext.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> DeleteBookingAsync(Guid id)
        {
            var existingBooking = await dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBooking == null)
            {
                return null;
            }

            dbContext.Bookings.Remove(existingBooking);
            dbContext.SaveChanges();
            return existingBooking; 
        }

        public async Task<List<Booking>> GetAllBookingAsync()
        {
            return await dbContext.Bookings.ToListAsync();
        }
    }
}
