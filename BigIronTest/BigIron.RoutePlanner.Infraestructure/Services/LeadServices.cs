using BigIron.RoutePlanner.Application.DTOs;
using BigIron.RoutePlanner.Domain.Entities;
using BigIron.RoutePlanner.Domain.Repositories;
using BigIron.RoutePlanner.Domain.Services;

namespace BigIron.RoutePlanner.Application.Services
{
    public class LeadServices : ILeadService
    {
        private readonly ILeadRepository _repo;
        private readonly IRouteOptimizer _optimizer;

        public LeadServices(ILeadRepository repo, IRouteOptimizer optimizer)
        {
            _repo = repo;
            _optimizer = optimizer;
        }

        public async Task<UploadResultDto> UploadLeadsAsync(IEnumerable<Lead> leads)
        {
            if (leads is null)
                throw new ArgumentNullException(nameof(leads));

            var list = leads.ToList();
            await _repo.AddRangeAsync(list);

            return new UploadResultDto(list.Count, list.Count);
        }

        public async Task<RouteResult> GenerateRouteAsync(RouteRequestDto request)
        {
            if (request.HomeLat is < -90 or > 90)
                throw new ArgumentException("Latitude must be between -90 and 90.");

            if (request.HomeLng is < -180 or > 180)
                throw new ArgumentException("Longitude must be between -180 and 180.");

            var leads = await _repo.GetAllAsync();

            if (!leads.Any())
                throw new InvalidOperationException("No leads available.");

            return await _optimizer.OptimizeAsync(request.HomeLat, request.HomeLng, leads);
        }

        public Task ClearLeadsAsync() => _repo.ClearAsync();

    }
}
