using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.DAL.Models
{
    public class Property
    {
        public Guid PropertyId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string GoogleMapsUrl { get; set; } = string.Empty;

        public PropertyType PropertyType { get; set; }
        public PropertyPurpose PropertyPurpose { get; set; }
        public PropertyStatus PropertyStatus { get; set; }

        public decimal Price {  get; set; }
        public float Square {  get; set; }
        public string Description { get; set; } = string.Empty;

        public DateTime DateListed { get; set; }

        public Guid AgentId { get; set; }
        public Agent Agent { get; set; }

        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public Amenity Amenity { get; set; }
        public ICollection<PropertyGallery> PropertyGalleries { get; set; } = new List<PropertyGallery>();
        public ICollection<LikeProperty> LikeEntities { get; set; } = new List<LikeProperty>(); //new added

        public Guid? CompoundId { get; set; }
        public Compound? Compound { get; set; }

        //public CommercialProperty CommercialProperty { get; set; }


    }
}
