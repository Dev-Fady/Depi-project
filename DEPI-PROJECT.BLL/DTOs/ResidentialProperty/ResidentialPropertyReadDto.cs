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

        public List<PropertyGalleryReadDto>? Galleries { get; set; }

        public AmenityReadDto Amenity { get; set; }
        //this part is new --> count of likes and is liked by user
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
    }
}
