using DEPI_PROJECT.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace DEPI_PROJECT.DAL.Models
{
    public class User : IdentityUser<Guid>
    {
        public Guid UserId { get => base.Id; set => base.Id = value; }
        public DateTime DateJoined { get; set; }
        
        // Navigation properties
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
