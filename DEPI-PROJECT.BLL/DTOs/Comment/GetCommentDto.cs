using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Dtos.Comment
{
    public class GetCommentDto
    {
        public Guid CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime DateComment { get; set; }
        public Guid UserID { get; set; }
        public Guid PropertyId { get; set; }
    }
}
