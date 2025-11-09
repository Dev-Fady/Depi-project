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
        public Task<ResponseDto<GetCommentDto?>> AddComment(Guid UserId, AddCommentDto commentDto);
        public Task<ResponseDto<bool>> DeleteComment(Guid UserId, Guid CommentId);
        public Task<ResponseDto<bool>> UpdateComment(Guid UserId, UpdateCommentDto commentDto , Guid CommentId);
        public Task<ResponseDto<PagedResultDto<GetCommentDto?>>> GetAllCommentsByPropertyId(CommentQueryDto queryDto);
        public Task<ResponseDto<GetCommentDto?>> GetCommentById(Guid commentId);
        public Task<ResponseDto<int>> CountAllComments(Guid PropertyId);
    }
}
