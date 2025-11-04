using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Models
{
    public class LikeEntity
    {
        public Guid LikeEntityId { get; set; }
        public Guid? UserID { get; set; }
        public Guid? CommentId { get; set; }
        public Guid? PropertyId { get; set; }

        public Property? Property { get; set; }
        public User? User { get; set; }
        public Comment? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
