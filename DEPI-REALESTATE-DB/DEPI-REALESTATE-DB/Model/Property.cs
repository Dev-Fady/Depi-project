using DEPI_REALESTATE_DB.Model.Enums;

namespace DEPI_REALESTATE_DB.Model
{
    public class Property
    {
        public Guid PropertyId { get; set; }
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

        public Guid AgentId { get; set; }
        public Agent Agent { get; set; }

        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public Amenity Amenity { get; set; }
    }
}
