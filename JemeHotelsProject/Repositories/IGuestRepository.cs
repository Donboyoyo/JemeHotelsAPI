using JemeHotelsProject.Models.Domain;

namespace JemeHotelsProject.Repositories
{
    public interface IGuestRepository
    {
        Task<List<Guest>>  GetAllAsync(int pageNumber = 1, int pageSize = 5);

        Task<Guest> GetByIdAsync(Guid id);

        Task<Guest> CreateAsync(Guest guest);
    }
}
