using BigIron.RoutePlanner.Domain.Entities;
using BigIron.RoutePlanner.Domain.Repositories;

namespace BigIron.RoutePlanner.Infraestructure.Repositories
{
    public class InMemoryLeadRepository : ILeadRepository
    {
            private readonly List<Lead> _data = new();
            private readonly SemaphoreSlim _lock = new(1, 1);

            public async Task AddRangeAsync(IEnumerable<Lead> leads)
            {
                await _lock.WaitAsync();
                try
                {
                    _data.AddRange(leads);
                }
                finally
                {
                    _lock.Release();
                }
            }

            public async Task<IReadOnlyList<Lead>> GetAllAsync()
            {
                await _lock.WaitAsync();
                try
                {
                    return _data.ToList().AsReadOnly();
                }
                finally
                {
                    _lock.Release();
                }
            }

            public async Task ClearAsync()
            {
                await _lock.WaitAsync();
                try
                {
                    _data.Clear();
                }
                finally
                {
                    _lock.Release();
                }
            }
    }
}
