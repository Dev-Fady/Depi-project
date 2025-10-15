using DEPI_REALESTATE_DB.Model.Enums;

namespace DEPI_REALESTATE_DB.Model
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string Phone { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime DateJoined { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
