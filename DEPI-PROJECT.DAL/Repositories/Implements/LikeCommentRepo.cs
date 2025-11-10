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
    public class LikeCommentRepo : ILikeCommentRepo
    {
        private readonly AppDbContext _appDbContext;
        public LikeCommentRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> AddLikeComment(LikeComment likeComment)
        {
            _appDbContext.LikeComments.Add(likeComment);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountLikesByCommentId(Guid commentId)
        {
            var count = await _appDbContext.LikeComments.Where(LC => LC.CommentId == commentId).CountAsync();
            return count;
        }

        public async Task<bool> DeleteLikeComment(LikeComment likeComment)
        {
             _appDbContext.LikeComments.Remove(likeComment);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<LikeComment?> GetLikeCommentByUserAndCommentId(Guid userId, Guid commentId)
        {
            var likeComment = await _appDbContext.LikeComments
                                    .FirstOrDefaultAsync(LC => LC.CommentId == commentId && LC.UserID == userId);
            return likeComment;
        }
        public IQueryable<LikeComment>? GetAllLikesByCommentId()
        {
            var likes = _appDbContext.LikeComments;
            return likes;
        }
    }
}
