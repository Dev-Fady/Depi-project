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
        public async Task<bool> AddItemInWishList(Wishlist wishlist)
        {
            _appDbContext.Wishlists.Add(wishlist);
            return await _appDbContext.SaveChangesAsync() > 0; //SaveChangesAsync returns number of affected rows
        }

        public async Task<bool> DeleteItemInWishList(Guid UserId, Guid ListingID)
        {
            var wishlist = await _appDbContext.Wishlists.FirstOrDefaultAsync(WL => WL.UserID == UserId && WL.ListingID == ListingID) ; //findAsync is used to find entity by primary key
            if (wishlist == null)
            {
                return false;
            }
            _appDbContext.Wishlists.Remove(wishlist);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public IQueryable<Wishlist> GetAllWishList()
        {
            var WishLists = _appDbContext.Wishlists.Include(R => R.Property);
            return WishLists; //cannot use await here as IQueryable is not awaitable --> not accessing database yet
        }

        public async Task<Wishlist?> GetWishList(Guid UserId, Guid PropertyId)
        {
            var wishlist = await _appDbContext.Wishlists.Include(R => R.Property).FirstOrDefaultAsync(WL => WL.UserID == UserId && WL.PropertyID == PropertyId);
            return wishlist;
    
        }
    }
}
