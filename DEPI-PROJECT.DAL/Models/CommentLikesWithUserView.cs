using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Models
{
    public class CommentLikesWithUserView
    {
        public Guid CommentId { get; set; }
       public int LikesCount { get; set; }
       public bool IsLiked { get; set; }
    }
}
