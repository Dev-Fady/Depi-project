using DEPI_PROJECT.BLL.DTOs.Amenity;
using DEPI_PROJECT.BLL.DTOs.Compound;
using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.ResidentialProperty
{
    public class ResidentialPropertyReadDto : PropertyResponseDto
    {
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int? Floors { get; set; }
        public KitchenType KitchenType { get; set; }

    }
}
