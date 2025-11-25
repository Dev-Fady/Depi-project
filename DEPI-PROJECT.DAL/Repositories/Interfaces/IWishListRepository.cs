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
        public Task<bool> AddItemInWishList(Wishlist wishlist);
        public Task<bool> DeleteItemInWishList(Guid UserId, Guid ListingID);

        public IQueryable<Wishlist> GetAllWishList();

        public Task<Wishlist?> GetWishList(Guid UserId , Guid PropertyId);

    }
}
