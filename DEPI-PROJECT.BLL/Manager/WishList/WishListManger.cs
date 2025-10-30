using DEPI_PROJECT.BLL.Dtos;
using DEPI_PROJECT.DAL.Repository.WishList;
using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DEPI_PROJECT.BLL.Manager.WishList
{
    public class WishListManger : IWishListManger
    {
        private readonly IWishListRepository _wishListRepository;
        public WishListManger(IWishListRepository wishListRepository) 
        {
            _wishListRepository = wishListRepository;
        }
        public async Task<bool> AddWishList(AddWishListDto wishlistDto)
        {
            if ( wishlistDto.UserID == Guid.Empty)
            {
                throw new ArgumentException("UserID cannot be empty.");
            }
            if (wishlistDto.PropertyID == Guid.Empty)
            {
                throw new ArgumentException("PropertyID cannot be empty.");
            }

            var wishlist = new Wishlist
            {
                UserID = wishlistDto.UserID,
                PropertyID = wishlistDto.PropertyID,
                CreatedAt = DateTime.UtcNow  // Use UTC time for consistency --> Converted to UTC (Coordinated Universal Time)
            };

            bool exists = await _wishListRepository.IsWishListFound(wishlist.UserID, wishlist.PropertyID);
            if (exists)
            {
                throw new InvalidOperationException("Wishlist already exists.");
            }
            return await _wishListRepository.AddWishList(wishlist);
        }

        public async Task<bool> DeleteWishList(DeleteWishListDto wishlistDto)
        {
            if (wishlistDto.UserID == Guid.Empty)
            {
                throw new ArgumentException("UserID cannot be empty.");
            }
            if (wishlistDto.PropertyID == Guid.Empty)
            {
                throw new ArgumentException("PropertyID cannot be empty.");
            }
            bool result =  await _wishListRepository.DeleteWishList(wishlistDto.UserID, wishlistDto.PropertyID);
            if (!result)
            {
                throw new InvalidOperationException("Wishlist not found.");
            }
            return result;
        }

        public async Task<IEnumerable<GetAllWishListDto>>? GetAllWishList(Guid UserId)
        {
            var Result = _wishListRepository.GetAllWishList(UserId).Include(R => R.Property);

            var WishListDtos = await Result.Select(R => new GetAllWishListDto 
                { 
                    ListingID = R.ListingID,
                    UserID = R.UserID,
                    PropertyID = R.PropertyID,
                    Price = R.Property.Price,
                    Title = R.Property.Address + "-" + R.Property.City
                }).ToListAsync();

            return WishListDtos;
        }

        public async Task<bool> IsWishListFound(CheckWishListDto checkWishListDto)
        {
            if (checkWishListDto.UserID == Guid.Empty)
            {
                throw new ArgumentException("UserID cannot be empty.");
            }
            if (checkWishListDto.PropertyID == Guid.Empty)
            {
                throw new ArgumentException("PropertyID cannot be empty.");
            }
            return await _wishListRepository.IsWishListFound(checkWishListDto.UserID, checkWishListDto.PropertyID);
        }
    }
}
