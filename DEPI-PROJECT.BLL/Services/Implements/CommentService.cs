using AutoMapper;
using DEPI_PROJECT.BLL.Common;
using DEPI_PROJECT.BLL.Dtos.Comment;
using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.BLL.Exceptions;
using DEPI_PROJECT.BLL.Extensions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ILikeCommentRepo _likeCommentRepo;
        private readonly IPropertyService _propertyService;

        public CommentService(ICommentRepository commentRepository,
                            IMapper mapper,
                            ILikeCommentRepo likeCommentRepo,
                            IPropertyService propertyService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _likeCommentRepo = likeCommentRepo;
            _propertyService = propertyService;
        }

        public async Task<ResponseDto<CommentGetDto?>> AddComment(Guid UserId, CommentAddDto commentDto)
        {
            if (UserId == Guid.Empty || commentDto == null)
            {
                throw new BadRequestException("User Id or comment cannot be null");
            }

            if (await _propertyService.CheckPropertyExist(commentDto.PropertyId) == false)
            {
                throw new NotFoundException($"No property found with Id {commentDto.PropertyId}");
            }

            var CommentEntity = _mapper.Map<Comment>(commentDto);
            CommentEntity.UserID = UserId;
            CommentEntity.DateComment = DateTime.UtcNow;

            var Result = await _commentRepository.AddComment(CommentEntity);
            if (!Result)
            {
                throw new Exception("An unexpected error ocurred while adding comment, please try again");
            }
            var getComment = _mapper.Map<CommentGetDto>(CommentEntity);
            return new ResponseDto<CommentGetDto?>()
            {
                IsSuccess = true,
                Message = "Comment added successfully",
                Data = getComment
            };

        }

        public async Task<ResponseDto<int>> CountAllComments(Guid PropertyId)
        {
            if (PropertyId == Guid.Empty)
            {
                throw new BadRequestException("Property Id cannot be null");
            }
            
            if (await _propertyService.CheckPropertyExist(PropertyId) == false)
            {
                throw new NotFoundException($"No property found with Id {PropertyId}");
            }

            var count = await _commentRepository.CountAllComments(PropertyId);
            return new ResponseDto<int>()
            {
                IsSuccess = true,
                Message = "Comments counted successfully",
                Data = count
            };
        }

        public async Task<ResponseDto<bool>> DeleteComment(Guid UserId, Guid CommentId)
        {
            if (UserId == Guid.Empty || CommentId == Guid.Empty)
            {
                throw new BadRequestException("User Id or comment Id cannot be null");
            }
            var comment = await _commentRepository.GetCommentById(CommentId);
            if (comment == null)
            {
                throw new NotFoundException($"No comment found with ID {CommentId}");
            }

            CommonFunctions.EnsureAuthorized(comment.UserID);

            var Result = await _commentRepository.DeleteComment(comment);
            if (!Result)
            {
                throw new Exception("An error occurred while deleting the comment, please try again");
            }
            return new ResponseDto<bool>()
            {
                IsSuccess = true,
                Message = "Comment deleted successfully",
                Data = true
            };

        }

        public async Task<ResponseDto<PagedResultDto<CommentGetDto?>>> GetAllCommentsByPropertyId(Guid CurrentUserId , Guid PropertyId, CommentQueryDto queryDto)
        {
            //call all comments 
            var comments = _commentRepository.GetAllCommentsByPropertyId(PropertyId);

            //count total comments after filter
            var totalComments = await comments.CountAsync();
            
            //apply order and pagination
            var orderedComments = comments.OrderByExtended(new List<Tuple<bool, Expression<Func<Comment, object>>>>
                                                    {
                                                     new (queryDto.OrderBy == OrderByCommentOptions.DateComment, c => c.DateComment),
                                                    },
                                                  queryDto.IsDesc
                                                  );
            var pagedComments =  orderedComments
                                .Paginate(queryDto);

            //execute query
            var Result = await pagedComments.ToListAsync();
            var mappedcomments =  _mapper.Map<IEnumerable<CommentGetDto>>(Result);

            //Add Islike , count for each comment
            var CommentIds = mappedcomments.Select(c => c.CommentId).ToList();
            var CountCommentDic = await _likeCommentRepo.GetAllLikesByCommentsId(CommentIds)
                                    .GroupBy(lc => lc.CommentId)
                                    .Select(n => new
                                    {
                                        CommentId = n.Key,
                                        Count = n.Count()
                                    })
                                    .ToDictionaryAsync(n => n.CommentId , n => n.Count);

            var IsLikedHash = await _likeCommentRepo.GetAllLikesByCommentsId(CommentIds)
                                    .Where(lc => lc.UserID == CurrentUserId)
                                    .Select(n => n.CommentId)
                                    .ToHashSetAsync();

            foreach(var comment in mappedcomments)
            {
                if(CountCommentDic.TryGetValue(comment.CommentId, out var count))
                {
                    comment.LikesCount = count;
                }
                else
                {
                    comment.LikesCount = 0;
                }
                if (IsLikedHash.Contains(comment.CommentId))
                {
                    comment.IsLiked = true;
                }
                else
                {
                    comment.IsLiked = false;
                }
            }

            //very slow --> make (N+1)problem
            ////Add Islike , count for each comment
            //foreach (var commentDto in mappedcomments)
            //{
            //    //count likes --> call likeCommentRepo
            //    commentDto.LikesCount = await _likeCommentRepo.CountLikesByCommentId(commentDto.CommentId);
            //    //check is liked by Current user
            //    commentDto.IsLiked = await _likeCommentRepo.GetLikeCommentByUserAndCommentId(CurrentUserId, commentDto.CommentId) !=null;

            //}

            //create paged result
            var pagedResult = new PagedResultDto<CommentGetDto>(mappedcomments , queryDto.PageNumber , totalComments , queryDto.PageSize);
            //return response 
            return new ResponseDto<PagedResultDto<CommentGetDto?>>()
            {
                IsSuccess = true,
                Message = "Comments retrieved successfully",
                Data = pagedResult
            };
        }

        public async Task<ResponseDto<CommentGetDto?>> GetCommentById(Guid CurrentUserId ,Guid commentId)
        {
            if (commentId == Guid.Empty)
            {
                throw new BadRequestException("Comment Id cannot be null");
            }
            var comment = await _commentRepository.GetCommentById(commentId);
            if (comment == null)
            {
                throw new NotFoundException($"No comment found with Id {commentId}");
            }
            var mappedComment = _mapper.Map<CommentGetDto>(comment);

            //count likes --> call likeCommentRepo
            mappedComment.LikesCount = await _likeCommentRepo.CountLikesByCommentId(mappedComment.CommentId);
            //check is liked by Current user
            mappedComment.IsLiked = await _likeCommentRepo.GetLikeCommentByUserAndCommentId(CurrentUserId, mappedComment.CommentId) != null;

            return new ResponseDto<CommentGetDto?>()
            {
                IsSuccess = true,
                Message = "Comment retrieved successfully",
                Data = mappedComment
            };
        }


        public async Task<ResponseDto<bool>> UpdateComment(Guid UserId, CommentUpdateDto commentDto, Guid CommentId)
        {
            if (UserId == Guid.Empty || commentDto == null || CommentId == Guid.Empty)
            {
                throw new BadRequestException("User id, commentId cannot be null");
            }
            var existingComment = await _commentRepository.GetCommentById(CommentId);
            if (existingComment == null)
            {
                throw new NotFoundException($"No comment found with Id {CommentId}");

            }
            
            CommonFunctions.EnsureAuthorized(existingComment.UserID);

            _mapper.Map(commentDto, existingComment); // Map updated fields to existing comment (source , Destination)
            var Result = await _commentRepository.UpdateComment(existingComment);
            if (!Result)
            {
                throw new Exception("An error occurred while updating the comment, please try again");
            }
            return new ResponseDto<bool>()
            {
                IsSuccess = true,
                Message = "Comment updated successfully",
                Data = true
            };

        }
    }

}
