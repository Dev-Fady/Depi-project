using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RealEstateBroker.DAL.Models
{
    public class Comment
    {
        public Guid CommentID { get; set; }
        public Guid PropertyID { get; set; }
        public Guid UserID { get; set; }
        public bool isLiked { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }

        public virtual Property Property { get; set; }
        public virtual User User { get; set; }
    }
}
