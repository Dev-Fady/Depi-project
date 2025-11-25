namespace DEPI_PROJECT.DAL.Models
{
    public class PropertyGallery
    {
        public Guid MediaId { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
