using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface IWishListRepository
    {
        public Task<bool> AddWishList(Wishlist wishlist);
        public Task<bool> DeleteWishList(Guid UserId, Guid PropertyId);

        public IQueryable<Wishlist> GetAllWishList(Guid UserId);

        public Task<bool> IsWishListFound(Guid UserId , Guid PropertyId);

    }
}
