using BigIron.RoutePlanner.Application.DTOs;
using BigIron.RoutePlanner.Domain.Entities;
using BigIron.RoutePlanner.Domain.Services;

namespace BigIron.RoutePlanner.Application.Services
{
    public interface ILeadService
    {
        Task<UploadResultDto> UploadLeadsAsync(IEnumerable<Lead> leads);

        /// <summary>
        /// Generates an optimized visit route based on the user's home location.
        /// </summary>
        Task<RouteResult> GenerateRouteAsync(RouteRequestDto request);
    }
}
