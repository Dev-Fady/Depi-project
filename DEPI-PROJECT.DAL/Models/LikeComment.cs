using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Models
{
    public class LikeComment
    {
        public Guid LikeCommentId { get; set; }
        public Guid UserID { get; set; }
        public Guid CommentId { get; set; }

        public Comment? Comment { get; set; }
        public User? User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
