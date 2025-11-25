namespace DEPI_PROJECT.BLL.DTOs.PropertyGallery
{
    public class PropertyGalleryReadDto
    {
        public required Guid MediaId { get; set; }
        public required Guid PropertyId { get; set; }
        public required string ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime UploadedAt { get; set; }
    }

}
