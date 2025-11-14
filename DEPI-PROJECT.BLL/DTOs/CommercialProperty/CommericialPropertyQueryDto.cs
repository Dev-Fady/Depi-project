using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.DTOs.Query;
using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.CommercialProperty
{
    public class CommercialPropertyQueryDto : PagedQueryDto
    {
        public Guid? UserId { get; set; }
        public string? BusinessType { get; set; }
        public int? FloorNumber { get; set; }
        public bool? HasStorage { get; set; }
    }
}
    