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
            var result = await _appDbContext.Comments.Where(c => c.PropertyId == propertyId).CountAsync();
            return result;
        }

        public async Task<bool> DeleteComment(Comment comment)
        {
            _appDbContext.Comments.Remove(comment);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public  IQueryable<Comment>? GetAllCommentsByPropertyId()
        {
            var comments = _appDbContext.Comments;
            return comments;
        }

        public async Task<Comment?> GetCommentById(Guid commentId)
        {
            var comment = await _appDbContext.Comments.FindAsync(commentId);
            return comment;
        }

        public async Task<bool> UpdateComment(Comment comment)
        {
            _appDbContext.Update(comment);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
