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
        public Task<bool> DeleteComment(Guid commentId);
        public Task<bool> UpdateComment();
        public Task<IEnumerable<Comment>>? GetAllCommentsByPropertyId(Guid propertyId);
        public Task<Comment?> GetCommentById(Guid commentId);
        public Task<bool> LikeComment();
        public Task<int> CountAllComments(Guid propertyId);
    }
}
