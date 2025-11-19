using BigIron.RoutePlanner.Domain.Entities;

namespace BigIron.RoutePlanner.Application.Services
{
    public interface ICsvLeadParser
    {
        IEnumerable<Lead> Parse(string csvContent);
    }
}
