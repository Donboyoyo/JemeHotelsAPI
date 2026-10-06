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

        public async Task<List<Room>> GetAllAsync(string? room_type = null)
        {
            var roomsQuery = dbContext.Rooms.AsQueryable();

            if (!string.IsNullOrWhiteSpace(room_type) && 
                (room_type.Equals("EnSuite", StringComparison.OrdinalIgnoreCase) || 
                (room_type.Equals("Basic", StringComparison.OrdinalIgnoreCase)))) {
                roomsQuery = roomsQuery.Where(r => r.roomType.ToLower() == room_type.ToLower());
            }

            return await roomsQuery.ToListAsync();
        }
    }
}
