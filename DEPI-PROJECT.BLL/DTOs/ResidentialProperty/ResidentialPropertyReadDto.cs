using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.ResidentialProperty
{
    public class ResidentialPropertyReadDto
    {
        public Guid PropertyId { get; set; }

        public string City { get; set; }
        public string Address { get; set; }
        public string GoogleMapsUrl { get; set; }

        public PropertyType PropertyType { get; set; }
        public PropertyPurpose PropertyPurpose { get; set; }
        public PropertyStatus PropertyStatus { get; set; }

        public decimal Price { get; set; }
        public float Square { get; set; }
        public string Description { get; set; }

        public DateTime DateListed { get; set; }

        public string AgentName { get; set; }
        public string? CompoundName { get; set; }

        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int? Floors { get; set; }
        public KitchenType KitchenType { get; set; }

        public List<string> GalleryUrls { get; set; } = new();
    }
}
