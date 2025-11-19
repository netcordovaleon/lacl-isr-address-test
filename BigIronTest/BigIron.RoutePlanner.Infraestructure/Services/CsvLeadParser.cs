using BigIron.RoutePlanner.Application.Services;
using BigIron.RoutePlanner.Domain.Entities;
using System.Globalization;

namespace BigIron.RoutePlanner.Infraestructure.Services
{
    public class CsvLeadParser : ICsvLeadParser
    {
        public IEnumerable<Lead> Parse(string csvContent)
        {

            using var reader = new StringReader(csvContent);

            var leadsParser = new List<Lead>();

            string? line;
            bool isHeader = true;

            while ((line = reader.ReadLine()) is not null)
            {
                if (isHeader)
                {
                    isHeader = false;
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = SplitCsvLine(line).ToArray();
                //if (parts.Length < 5)
                //    throw new FormatException("CSV must contain at least 5 columns.");

                leadsParser.Add( new Lead(
                    Guid.NewGuid(),
                    parts[0],
                    parts[1],
                    double.Parse(parts[2], CultureInfo.InvariantCulture),
                    double.Parse(parts[3], CultureInfo.InvariantCulture)
                ));
            }

            return leadsParser;
        }

        private static IEnumerable<string> SplitCsvLine(string line)
        {
            bool inQuotes = false;
            var current = new System.Text.StringBuilder();

            foreach (var ch in line)
            {
                if (ch == '"' )
                {
                    inQuotes = !inQuotes;
                    continue;
                }

                if (ch == ',' && !inQuotes)
                {
                    yield return current.ToString().Trim();
                    current.Clear();
                }
                else
                {
                    current.Append(ch);
                }
            }

            yield return current.ToString().Trim();
        }

    }
}
