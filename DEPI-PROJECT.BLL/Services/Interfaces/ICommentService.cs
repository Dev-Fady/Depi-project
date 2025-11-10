using DEPI_PROJECT.BLL.Dtos.Comment;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<ResponseDto<CommentGetDto?>> AddComment(Guid UserId, CommentAddDto commentDto);
        public Task<ResponseDto<bool>> DeleteComment(Guid UserId, Guid CommentId);
        public Task<ResponseDto<bool>> UpdateComment(Guid UserId, CommentUpdateDto commentDto , Guid CommentId);
        public Task<ResponseDto<PagedResultDto<CommentGetDto?>>> GetAllCommentsByPropertyId(Guid CurrentUserId , CommentQueryDto queryDto);
        public Task<ResponseDto<CommentGetDto?>> GetCommentById(Guid CurrentUserId ,Guid commentId);
        public Task<ResponseDto<int>> CountAllComments(Guid PropertyId);
    }
}
