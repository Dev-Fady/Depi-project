namespace DEPI_REALESTATE_DB.Model
{
    public class PropertyGallery
    {
        public Guid MediaId { get; set; }
        public Guid PropertyId { get; set; }
        public Property Property { get; set; }

        public string ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
