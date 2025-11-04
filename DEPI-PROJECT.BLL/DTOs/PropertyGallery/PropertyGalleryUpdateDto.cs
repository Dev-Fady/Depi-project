using Microsoft.AspNetCore.Http;

namespace DEPI_PROJECT.BLL.DTOs.PropertyGallery
{
    public class PropertyGalleryUpdateDto
    {
        public Guid MediaId { get; set; }
        public Guid PropertyId { get; set; }
        public List<IFormFile>? GalleryFiles { get; set; } = new();
    }
}
