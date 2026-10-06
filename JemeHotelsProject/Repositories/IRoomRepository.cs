using JemeHotelsProject.Models.Domain;

namespace JemeHotelsProject.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllAsync(string? room_type = null);


        Task<Room> CreateAsync(Room room);
    }
}
