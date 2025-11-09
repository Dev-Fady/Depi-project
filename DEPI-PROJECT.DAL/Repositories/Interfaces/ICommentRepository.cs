using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        public Task<bool> AddComment(Comment comment);
        public Task<bool> DeleteComment(Comment comment);
        public Task<bool> UpdateComment(Comment comment);
        public IQueryable<Comment>? GetAllCommentsByPropertyId();
        public Task<Comment?> GetCommentById(Guid commentId);
        public Task<int> CountAllComments(Guid propertyId);
    }
}
