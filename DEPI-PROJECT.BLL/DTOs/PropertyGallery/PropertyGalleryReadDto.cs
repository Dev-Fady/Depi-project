namespace DEPI_PROJECT.BLL.DTOs.PropertyGallery
{
    public class PropertyGalleryReadDto
    {
        public Guid MediaId { get; set; }
        public Guid PropertyId { get; set; }
        public string ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime UploadedAt { get; set; }
    }

}
