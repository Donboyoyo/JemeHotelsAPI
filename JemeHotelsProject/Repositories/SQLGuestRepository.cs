using JemeHotelsProject.Data;
using JemeHotelsProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JemeHotelsProject.Repositories
{
    public class SQLGuestRepository : IGuestRepository
    {
        private readonly JemeHotelsDbContext dbContext;

        public SQLGuestRepository(JemeHotelsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guest> CreateAsync(Guest guest)
        {
            await dbContext.Guests.AddAsync(guest);
            await dbContext.SaveChangesAsync();
            return guest;

        }

        public async Task<List<Guest>> GetAllAsync(int pageNumber = 1, int pageSize = 5)
        {

            // Implementing pagination
            var skipResults = (pageNumber -1 ) * pageSize;

            return await dbContext.Guests.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Guest> GetByIdAsync(Guid id)
        {
            return await dbContext.Guests.FindAsync(id);
        }
    }
}
