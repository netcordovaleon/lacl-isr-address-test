
using BigIron.RoutePlanner.Domain.Services;

namespace BigIron.RoutePlanner.Application.Services
{
    public interface IExport
    {
         byte[] Export(RouteResult sortedLeads);
    }
}
