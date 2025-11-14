using DEPI_PROJECT.BLL.DTOs.Amenity;
using DEPI_PROJECT.BLL.DTOs.Compound;
using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.Property
{
    public class PropertyResponseDto
    {
        public Guid PropertyId { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string GoogleMapsUrl { get; set; }

        public PropertyType PropertyType { get; set; }
        public PropertyPurpose PropertyPurpose { get; set; }
        public PropertyStatus PropertyStatus { get; set; }

        public decimal Price {  get; set; }
        public float Square {  get; set; }
        public string Description { get; set; }

        public DateTime DateListed { get; set; }

        public Guid UserId { get; set; }
        // public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public AmenityReadDto Amenity { get; set; }
        public ICollection<PropertyGalleryReadDto> Galleries { get; set; } = new List<PropertyGalleryReadDto>();
        public CompoundReadDto? Compound { get; set; }

    }
}