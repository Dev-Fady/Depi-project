using Microsoft.AspNetCore.Http;

namespace DEPI_PROJECT.BLL.DTOs.PropertyGallery
{
    public class PropertyGalleryAddDto
    {
        public Guid PropertyId { get; set; }

        public List<IFormFile> MediaFiles { get; set; } = new();
    }
}
