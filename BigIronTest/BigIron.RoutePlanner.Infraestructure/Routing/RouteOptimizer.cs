using BigIron.RoutePlanner.Domain.Entities;
using BigIron.RoutePlanner.Domain.Services;

namespace BigIron.RoutePlanner.Infraestructure.Routing
{
    public class RouteOptimizer : IRouteOptimizer
    {
        public Task<RouteResult> OptimizeAsync(double homeLat, double homeLng, IEnumerable<Lead> leads)
        {
            var points = leads.ToList();

            var ordered = NearestNeighbor(homeLat, homeLng, points);
            var optimized = TwoOpt(ordered);

            var total = CalculateTotalDistance(homeLat, homeLng, optimized);

            return Task.FromResult(new RouteResult(optimized, total));
        }

        private static List<Lead> NearestNeighbor(double lat, double lng, List<Lead> leads)
        {
            var remaining = new List<Lead>(leads);
            var ordered = new List<Lead>();

            double currentLat = lat, currentLng = lng;

            while (remaining.Count > 0)
            {
                var next = remaining
                    .OrderBy(l => Haversine(currentLat, currentLng, l.Latitude, l.Longitude))
                    .First();

                ordered.Add(next);
                remaining.Remove(next);

                currentLat = next.Latitude;
                currentLng = next.Longitude;
            }

            return ordered;
        }

        private static List<Lead> TwoOpt(List<Lead> route)
        {
            bool improved;
            do
            {
                improved = false;

                for (int i = 1; i < route.Count - 2; i++)
                {
                    for (int j = i + 1; j < route.Count - 1; j++)
                    {
                        var newRoute = TwoOptSwap(route, i, j);
                        if (TotalRouteDistance(newRoute) < TotalRouteDistance(route))
                        {
                            route = newRoute;
                            improved = true;
                        }
                    }
                }
            } while (improved);

            return route;
        }

        private static List<Lead> TwoOptSwap(List<Lead> route, int i, int j)
        {
            var newRoute = route.ToList();
            newRoute.Reverse(i, j - i + 1);
            return newRoute;
        }

        private static double TotalRouteDistance(List<Lead> route)
        {
            double total = 0;
            for (int i = 0; i < route.Count - 1; i++)
            {
                total += Haversine(
                    route[i].Latitude, route[i].Longitude,
                    route[i + 1].Latitude, route[i + 1].Longitude);
            }
            return total;
        }

        private static double CalculateTotalDistance(double lat, double lng, List<Lead> route)
        {
            if (route.Count == 0) return 0;

            double total = Haversine(lat, lng, route[0].Latitude, route[0].Longitude);
            total += TotalRouteDistance(route);

            return total;
        }

        private static double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371;
            var dLat = DegreesToRad(lat2 - lat1);
            var dLon = DegreesToRad(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRad(lat1)) *
                    Math.Cos(DegreesToRad(lat2)) *
                    Math.Sin(dLon / 2) *
                    Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double DegreesToRad(double deg) => deg * (Math.PI / 180);
    }
}
