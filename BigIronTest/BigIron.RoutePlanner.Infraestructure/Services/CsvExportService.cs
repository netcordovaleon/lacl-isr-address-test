using BigIron.RoutePlanner.Application.Services;
using BigIron.RoutePlanner.Domain.Entities;
using BigIron.RoutePlanner.Domain.Services;
using System.Text;

namespace BigIron.RoutePlanner.Infraestructure.Services
{
    public class CsvExportService : IExport
    {
        public byte[] Export(RouteResult sortedLeads)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Id,Name,Address,Latitude,Longitude");

            int order = 1;

            foreach (var lead in sortedLeads.OrderedLeads)
            {
                sb.AppendLine(
                    $"{lead.Id}," +
                    $"{Escape(lead.Name)}," +
                    $"{Escape(lead.Address)}," +
                    $"{lead.Latitude}," +
                    $"{lead.Longitude}"
                );

                order++;
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private static string Escape(string value)
        {
            // Maneja comas y comillas
            if (value.Contains(',') || value.Contains('"'))
                return $"\"{value.Replace("\"", "\"\"")}\"";

            return value;
        }
    }
}
