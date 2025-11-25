using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Dtos.Comment
{
    public class CommentAddDto
    {
        public string? CommentText { get; set; }
        public required Guid PropertyId { get; set; }

    }
}
