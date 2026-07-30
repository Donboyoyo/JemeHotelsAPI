using JemeHotelsProject.Models.Domain;

namespace JemeHotelsProject.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllAsync();

        Task<List<Room>> GetAllAvailableAsync();

        Task<Room> CreateAsync(Room room);
    }
}
