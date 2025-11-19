namespace BigIron.RoutePlanner.Domain.Entities
{
    public class Lead
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Address { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public Lead(Guid id, string name, string address, double lat, double lng)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Lead name is required.");

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Lead address is required.");

            Id = id;
            Name = name.Trim();
            Address = address.Trim();
            Latitude = lat;
            Longitude = lng;
        }
    }
}
