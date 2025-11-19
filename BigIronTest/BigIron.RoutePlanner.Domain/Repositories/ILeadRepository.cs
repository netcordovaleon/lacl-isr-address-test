using BigIron.RoutePlanner.Domain.Entities;

namespace BigIron.RoutePlanner.Domain.Repositories
{
    public interface ILeadRepository
    {
        Task AddRangeAsync(IEnumerable<Lead> leads);
        Task<IReadOnlyList<Lead>> GetAllAsync();
        Task ClearAsync();
    }
}
