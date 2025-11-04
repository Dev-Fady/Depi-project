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
    public class WishListRepository : IWishListRepository
    {
        private readonly AppDbContext _appDbContext;

        // Dependency Injection via constructor
        public WishListRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // CRUD Operations
        public async Task<bool> AddWishList(Wishlist wishlist)
        {
            _appDbContext.Wishlists.Add(wishlist);
            return await _appDbContext.SaveChangesAsync() > 0; //SaveChangesAsync returns number of affected rows
        }

        public async Task<bool> DeleteWishList(Guid UserId, Guid PropertyId)
        {
            var wishlist = await _appDbContext.Wishlists.FirstOrDefaultAsync(WL => WL.UserID == UserId && WL.PropertyID == PropertyId) ; //findAsync is used to find entity by primary key
            if (wishlist == null)
            {
                return false;
            }
            _appDbContext.Wishlists.Remove(wishlist);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public IQueryable<Wishlist> GetAllWishList(Guid UserId)
        {
            var WishLists = _appDbContext.Wishlists.Where(WL => WL.UserID == UserId).OrderBy(WL =>WL.CreatedAt);
            return WishLists; //cannot use await here as IQueryable is not awaitable --> not accessing database yet
        }

        public async Task<bool> IsWishListFound(Guid UserId, Guid PropertyId)
        {
            var wishlist = await _appDbContext.Wishlists.AnyAsync(WL => WL.UserID == UserId && WL.PropertyID == PropertyId); //findAsync is used to find entity by primary key
            return wishlist;
            /*
            var wishlist = await _appDbContext.Wishlists.Where(WL => WL.UserID == UserId && WL.PropertyID == PropertyId).FirstOrDefaultAsync(); //findAsync is used to find entity by primary key
            if (wishlist == null)
            {
                return false;
            }
            return true;*/

        }
    }
}
