using DEPI_PROJECT.BLL.Dtos.Comment;
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
        public required Guid PropertyId { get; set; }
        public required string Title { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public required string GoogleMapsUrl { get; set; }

        public required PropertyType PropertyType { get; set; }
        public required PropertyPurpose PropertyPurpose { get; set; }
        public required PropertyStatus PropertyStatus { get; set; }

        public required decimal Price {  get; set; }
        public required float Square {  get; set; }
        public required string Description { get; set; }

        public DateTime DateListed { get; set; }

        public required Guid UserId { get; set; }
        public required ICollection<CommentGetDto> Comments { get; set; } = new List<CommentGetDto>();

        public required AmenityReadDto Amenity { get; set; }
        public required ICollection<PropertyGalleryReadDto> Galleries { get; set; } = new List<PropertyGalleryReadDto>();
        public CompoundReadDto? Compound { get; set; }

    }
}