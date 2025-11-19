namespace BigIron.RoutePlanner.Web.Models
{
    public class UploadViewModel
    {
        public IFormFile? LeadFile { get; set; }
        public double? HomeLat { get; set; } = 0;
        public double? HomeLng { get; set; } = 0;
    }
}
