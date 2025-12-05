using DEPI_PROJECT.BLL.Dtos.Like;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Exceptions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class LikeCommentService : ILikeCommentService
    {
        private readonly ILikeCommentRepo _LikeCommentRepo;
        private readonly ICommentRepository _commentRepo;
        public LikeCommentService(ILikeCommentRepo LikeCommentRepo , ICommentRepository commentRepo)
        {
            _LikeCommentRepo = LikeCommentRepo;
            _commentRepo = commentRepo;
        }
        public async Task<ResponseDto<ToggleResult>> ToggleLikeComment(Guid userId, Guid commentId)
        {
            if (userId == Guid.Empty || commentId == Guid.Empty)
            {
                throw new BadRequestException("User id and comment Id both cannot be null");
            }
            var comment = await _commentRepo.GetCommentById(userId, commentId);
            if (comment == null)
            {
                throw new NotFoundException($"No comment found with Id {commentId}");
            }

            var existingLike = await _LikeCommentRepo.GetLikeCommentByUserAndCommentId(userId, commentId);
            if (existingLike == null)
            {
                var newLike = new LikeComment
                {
                    UserID = userId,
                    CommentId = commentId,
                    CreatedAt = DateTime.UtcNow
                };
                var statusAdd = await _LikeCommentRepo.AddLikeComment(newLike);
                if (!statusAdd)
                {
                    throw new Exception("An error occured while liking the comment, please try again");
                }
                return new ResponseDto<ToggleResult>()
                {
                    IsSuccess = true,
                    Message = "Comment liked successfully",
                    Data = ToggleResult.Added
                };
            }
            var statusDelete = await _LikeCommentRepo.DeleteLikeComment(existingLike);
            if (!statusDelete)
            {
                throw new Exception("An error occured while disliking the comment, please try again");

            }
            return new ResponseDto<ToggleResult>()
            {
                IsSuccess = true,
                Message = "Comment disliked successfully",
                Data = ToggleResult.Deleted
            };
        }
             
    }
}
