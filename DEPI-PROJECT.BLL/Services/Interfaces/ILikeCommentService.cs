using DEPI_PROJECT.BLL.Dtos.Comment;
using DEPI_PROJECT.BLL.Dtos.Like;
using DEPI_PROJECT.BLL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface ILikeCommentService
    {
        public Task<ResponseDto<ToggleResult>> ToggleLikeComment(Guid userId, Guid commentId);
        public Task AddLikesCountAndIsLike(Guid UserId, List<CommentGetDto> mappedData);
    }
}
