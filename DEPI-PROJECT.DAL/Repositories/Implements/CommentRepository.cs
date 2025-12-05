using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Repositories.Implements
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _appDbContext;
        public CommentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> AddComment(Comment comment)
        {
             await _appDbContext.Comments.AddAsync(comment);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountAllComments(Guid propertyId)
        {
            var result = await _appDbContext.Comments
                                            .Where(c => c.PropertyId == propertyId)
                                            .CountAsync();
            return result;
        }

        public async Task<bool> DeleteComment(Comment comment)
        {
            _appDbContext.Comments.Remove(comment);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public IQueryable<Comment> GetAllCommentsByPropertyId(Guid propertyId)
        {
            var comments = _appDbContext.Comments
                            .Where(c => c.PropertyId == propertyId)
                            .Select(C => new Comment
                            {
                                CommentId = C.CommentId,
                                UserID = C.UserID,
                                CommentText = C.CommentText,
                                DateComment = C.DateComment,
                                PropertyId = C.PropertyId,

                                Property = C.Property,
                                User = C.User,
                                LikeComments = C.LikeComments,

                                LikesCount = _appDbContext.CommrentLikesWithUserViews
                                                .Where(c => c.CommentId == C.CommentId)
                                                .Select(c => c.LikesCount)
                                                .FirstOrDefault(),



                               IsLiked = _appDbContext.CommrentLikesWithUserViews
                                            .Where(c => c.CommentId == C.CommentId)
                                            .Select(c => c.IsLiked)
                                            .FirstOrDefault()


                            });
        
            return comments;
        }

        public async Task<Comment?> GetCommentById(Guid commentId)
        {
            var comment = await _appDbContext.Comments
                                .Select(C => new Comment
                                {
                                    CommentId = C.CommentId,
                                    UserID = C.UserID,
                                    CommentText = C.CommentText,
                                    DateComment = C.DateComment,
                                    PropertyId = C.PropertyId,

                                    Property = C.Property,
                                    User = C.User,
                                    LikeComments = C.LikeComments,

                                    LikesCount = _appDbContext.CommrentLikesWithUserViews
                                            .Where(c => c.CommentId == C.CommentId)
                                            .Select(c => c.LikesCount)
                                            .FirstOrDefault(),



                                    IsLiked = _appDbContext.CommrentLikesWithUserViews
                                            .Where(c => c.CommentId == C.CommentId)
                                            .Select(c => c.IsLiked)
                                            .FirstOrDefault()


                                }).FirstOrDefaultAsync(c => c.CommentId == commentId);
            return comment;
        }

        public async Task<bool> UpdateComment(Comment comment)
        {
            _appDbContext.Update(comment);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
     

    }
}
