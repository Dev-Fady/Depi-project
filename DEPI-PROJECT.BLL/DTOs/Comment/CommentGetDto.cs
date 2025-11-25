using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Dtos.Comment
{
    public class CommentGetDto
    {
        public required Guid CommentId { get; set; }
        public string? CommentText { get; set; }
        public required DateTime DateComment { get; set; }
        public required Guid UserID { get; set; }
        public required Guid PropertyId { get; set; }
        public required bool IsLiked { get; set; }
        public required int LikesCount { get; set; }
    }
}
