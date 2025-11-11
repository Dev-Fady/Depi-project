using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface ILikePropertyRepo
    {
        public Task<bool> AddLikeProperty(LikeProperty likeProperty);
        public Task<bool> DeleteLikeProperty(LikeProperty likeProperty);
        public Task<int> CountLikesByPropertyId(Guid propertyId);
        public Task<LikeProperty?> GetLikePropertyByUserAndPropertyId(Guid userId, Guid propertyId);
        public IQueryable<LikeProperty>? GetAllLikesByPropertyId(Guid PropertyId);
        public IQueryable<LikeProperty>? GetAllLikesByPropertyIds(List<Guid> PropertyIds);
    }
}
