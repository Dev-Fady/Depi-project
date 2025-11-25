using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface ILikeCommentRepo
    {
        public Task<bool> AddLikeComment(LikeComment likeComment);
        public Task<bool> DeleteLikeComment(LikeComment likeComment);
        public Task<int> CountLikesByCommentId(Guid commentId);
        public Task<LikeComment?> GetLikeCommentByUserAndCommentId(Guid userId, Guid commentId);

        public IQueryable<LikeComment> GetAllLikesByCommentId(Guid CommentId);
        public IQueryable<LikeComment> GetAllLikesByCommentsId(List<Guid> CommentIds);

    }
}
