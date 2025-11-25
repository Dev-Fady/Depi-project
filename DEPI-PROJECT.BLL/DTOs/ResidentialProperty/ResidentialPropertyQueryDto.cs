using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.DTOs.Query;
using DEPI_PROJECT.DAL.Models.Enums;
using Humanizer;

namespace DEPI_PROJECT.BLL.DTOs.ResidentialProperty
{
    public class ResidentialPropertyQueryDto : PagedQueryDto
    {
        public Guid? UserId { get; set; } 
        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? Floors { get; set; }
        public KitchenType? KitchenType { get; set; }
    }
}