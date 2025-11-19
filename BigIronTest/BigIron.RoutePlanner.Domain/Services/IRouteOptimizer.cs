using BigIron.RoutePlanner.Domain.Entities;

namespace BigIron.RoutePlanner.Domain.Services
{
    public interface IRouteOptimizer
    {
        /// <summary>
        /// Generates an optimized route using provided home coordinates.
        /// </summary>
        Task<RouteResult> OptimizeAsync(double homeLat, double homeLng, IEnumerable<Lead> leads);
    }
}
