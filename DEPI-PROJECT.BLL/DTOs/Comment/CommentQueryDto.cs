using DEPI_PROJECT.BLL.DTOs.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Dtos.Comment
{
    public enum OrderByCommentOptions
    {
        DateComment   
    }
    public class CommentQueryDto : PagedQueryDto
    {
        public bool IsDesc { get; set; }
        public OrderByCommentOptions? OrderBy { get; set; }

    }
}
