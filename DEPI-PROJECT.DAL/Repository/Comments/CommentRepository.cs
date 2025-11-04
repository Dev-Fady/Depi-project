using DEPI_PROJECT.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Repository.Comments
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
            var result = await _appDbContext.Comments.Where(c => c.PropertyId == propertyId).CountAsync();
            return result;
        }

        public async Task<bool> DeleteComment(Guid commentId)
        {
           var comment =await GetCommentById(commentId); 
            if (comment == null)
            {
                return false;
            }
            _appDbContext.Comments.Remove(comment);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Comment>>? GetAllCommentsByPropertyId(Guid propertyId)
        {
            var comments = await _appDbContext.Comments.Where(c => c.PropertyId == propertyId).OrderByDescending(c => c.DateComment).ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetCommentById(Guid commentId)
        {
            var comment = await _appDbContext.Comments.FindAsync(commentId);
            return comment;
        }

        public async Task<bool> LikeComment()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateComment()
        {
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
