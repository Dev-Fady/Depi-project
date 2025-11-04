using DEPI_PROJECT.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace DEPI_PROJECT.DAL.Models
{
    public class User : IdentityUser<Guid>
    {
        public Guid UserId { get => base.Id; set => base.Id = value; }
        public DateTime DateJoined { get; set; }

        // Navigation properties
        public Agent? Agent { get; set; }
        public Broker? Broker { get; set; }
        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        //public ICollection<LikeEntity> LikeEntities { get; set; } = new List<LikeEntity>(); //new added
    }
}
