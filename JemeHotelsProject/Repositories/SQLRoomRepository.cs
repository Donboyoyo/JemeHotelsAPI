using AutoMapper;
using JemeHotelsProject.Data;
using JemeHotelsProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace JemeHotelsProject.Repositories
{
    public class SQLRoomRepository : IRoomRepository
    {
        private readonly JemeHotelsDbContext dbContext;

        public SQLRoomRepository(JemeHotelsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Room> CreateAsync(Room room)
        {


            await dbContext.Rooms.AddAsync(room);
            await dbContext.SaveChangesAsync();
            return room;
        }
        public async Task<List<Room>> GetAllAsync()
        {
            return await dbContext.Rooms.ToListAsync();

        }

        public async Task<List<Room>> GetAllAvailableAsync()
        {
            return await dbContext.Rooms.Where(room => room.isAvailable == true).ToListAsync();
        }
    }
}
