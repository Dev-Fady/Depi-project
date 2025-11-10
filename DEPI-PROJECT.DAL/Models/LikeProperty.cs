using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Models
{
    public class LikeProperty
    {
        public Guid LikeEntityId { get; set; }
        public Guid UserID { get; set; }
        public Guid PropertyId { get; set; }

        public Property? Property { get; set; }
        public User? User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
