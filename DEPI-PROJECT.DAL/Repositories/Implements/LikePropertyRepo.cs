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
    public class LikePropertyRepo : ILikePropertyRepo
    {
        private readonly AppDbContext _appDbContext;
        public LikePropertyRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> AddLikeProperty(LikeProperty likeProperty)
        {
            _appDbContext.LikeProperties.Add(likeProperty);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<int> CountLikesByPropertyId(Guid propertyId)
        {
            var count = await _appDbContext.LikeProperties.Where(LP => LP.PropertyId == propertyId).CountAsync();
            return count;
        }

        public async Task<bool> DeleteLikeProperty(LikeProperty likeProperty)
        {
            _appDbContext.LikeProperties.Remove(likeProperty);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<LikeProperty?> GetLikePropertyByUserAndPropertyId(Guid userId, Guid propertyId)
        {
            var likeProperty = await _appDbContext.LikeProperties
                                    .FirstOrDefaultAsync(LP => LP.PropertyId == propertyId && LP.UserID == userId);
            return likeProperty;
        }
        public IQueryable<LikeProperty>? GetAllLikesByPropertyId(Guid PropertyId)
        {
            var likes = _appDbContext.LikeProperties
                                    .Where(lp => lp.PropertyId == PropertyId);
            return likes;
        }
        public IQueryable<LikeProperty>? GetAllLikesByPropertyIds(List<Guid> PropertyIds)
        {
            var likes = _appDbContext.LikeProperties
                                    .Where(lp => PropertyIds.Contains(lp.PropertyId));
            return likes;
        }
        
    }
}
