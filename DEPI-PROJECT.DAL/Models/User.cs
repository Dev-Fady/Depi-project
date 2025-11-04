using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.DAL.Models
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
        public ICollection<LikeEntity> LikeEntities { get; set; } = new List<LikeEntity>(); //new added

    }
}
