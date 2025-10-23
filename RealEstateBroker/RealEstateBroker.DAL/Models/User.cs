using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RealEstateBroker.DAL.Enums.RoleOfUser;

namespace RealEstateBroker.DAL.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Role Role { get; set; }
        public DateTime DateJoined { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();

    }
}
