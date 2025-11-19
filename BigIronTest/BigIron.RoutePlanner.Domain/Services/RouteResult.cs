using BigIron.RoutePlanner.Domain.Entities;

namespace BigIron.RoutePlanner.Domain.Services
{
    public class RouteResult
    {
        public IReadOnlyList<Lead> OrderedLeads { get; }
        public double TotalDistanceKm { get; }
        public RouteResult(IReadOnlyList<Lead> leads, double distanceKm)
        {
            OrderedLeads = leads;
            TotalDistanceKm = distanceKm;
        }
    }
}
