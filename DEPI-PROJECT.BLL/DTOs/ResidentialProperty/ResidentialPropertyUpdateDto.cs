using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.ResidentialProperty
{
    public class ResidentialPropertyUpdateDto
    {
        public Guid? PropertyId { get; set; }

        public string? City { get; set; }
        public string? Address { get; set; }
        public string? GoogleMapsUrl { get; set; }

        public PropertyType? PropertyType { get; set; }
        public PropertyPurpose? PropertyPurpose { get; set; }
        public PropertyStatus? PropertyStatus { get; set; }

        public decimal? Price { get; set; }
        public float? Square { get; set; }
        public string? Description { get; set; }

        public int? Bedrooms { get; set; }
        public int? Bathrooms { get; set; }
        public int? Floors { get; set; }
        public KitchenType? KitchenType { get; set; }

        public Guid? CompoundId { get; set; }
    }


}
