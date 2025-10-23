namespace DEPI_PROJECT.DAL.Models
{
    public class Wishlist
    {
        public Guid ListingID { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }

        public Guid PropertyID { get; set; }
        public Property Property { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
