using AutoMapper;
using DEPI_PROJECT.BLL.Dtos.Comment;
using DEPI_PROJECT.BLL.Dtos.Wishists;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Extensions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly IMapper _mapper;

        public WishListService(IWishListRepository wishListRepository , IMapper mapper) 
        {
            _wishListRepository = wishListRepository;
            _mapper = mapper;
        }
        public async Task<ResponseDto<WishListGetDto?>> AddItemInWishList(Guid CurrentUserId, WishListAddDto wishlistDto)
        {
            if (CurrentUserId == Guid.Empty)
            {
                return new ResponseDto<WishListGetDto?>
                {
                    IsSuccess = false,
                    Message = "Invalid User ID.",
                    Data = null
                };
            }
            if (wishlistDto.PropertyID == Guid.Empty)
            {
                return new ResponseDto<WishListGetDto?>
                {
                    IsSuccess = false,
                    Message = "Invalid Property ID.",
                    Data = null
                };
            }
            //check if already exists
            var exists = await _wishListRepository.GetWishList(CurrentUserId, wishlistDto.PropertyID);
            if (exists != null)
            {
                return new ResponseDto<WishListGetDto?>
                {
                    IsSuccess = false,
                    Message = "This Item is already in your wishlist.",
                    Data = null
                };
            }

            var wishlist = _mapper.Map<Wishlist>(wishlistDto);
            wishlist.UserID = CurrentUserId;

            var Result = await _wishListRepository.AddItemInWishList(wishlist);
            if (!Result)
            {
                return new ResponseDto<WishListGetDto?>
                {
                    IsSuccess = false,
                    Message = "Failed to add Item.",
                    Data = null
                };
            }

            //not include property details in mapping so manually adding them
            var fullWishlist = await _wishListRepository.GetWishList(wishlist.UserID, wishlist.PropertyID);
            var mappedWishList = _mapper.Map<WishListGetDto>(fullWishlist);
            mappedWishList.Price = fullWishlist.Property.Price;
            mappedWishList.Title = fullWishlist.Property.Address + "-" + fullWishlist.Property.City;
            //not correct way to do it but due to time constraint doing it this way
            //addedWishList.Price = wishlist.Property.Price;
            //addedWishList.Title = wishlist.Property.Address + "-" + wishlist.Property.City;
            return new ResponseDto<WishListGetDto?>
            {
                IsSuccess = true,
                Message = "Item added successfully.",
                Data = mappedWishList
            };
        }

        public async Task<ResponseDto<bool>> DeleteItemInWishList(Guid CurrentUserId, WishListDeleteDto wishlistDto)
        {
            if (CurrentUserId == Guid.Empty)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Invalid User ID.",
                    Data = false
                };
            }
            if (wishlistDto.ListingID == Guid.Empty)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Invalid Listing ID.",
                    Data = false
                };
            }
            bool result =  await _wishListRepository.DeleteItemInWishList(CurrentUserId, wishlistDto.ListingID);
            if (!result)
            {
                return new ResponseDto<bool>
                {
                    IsSuccess = false,
                    Message = "Failed to delete Item.",
                    Data = false
                };
            }
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Successfuly deleted Item.",
                Data = true
            };
        }

        public async Task<ResponseDto<PagedResultDto<WishListGetDto?>>> GetAllWishList(Guid CurrentUserId, WishListQueryDto queryDto)
        {
            var Result = _wishListRepository.GetAllWishList();

            var FilteredResult = Result.Where(r => r.UserID == CurrentUserId);

            var Count = await FilteredResult.CountAsync();

            var OrderedResult = FilteredResult.OrderByExtended(new List<Tuple<bool, Expression<Func<Wishlist, object>>>>
                                            {
                                                new (queryDto.OrderBy == OrderByWishListOptions.CreatedAt, w => w.CreatedAt),
                                            },
                                            queryDto.IsDesc
                                            );

            var PaginateWishList = await OrderedResult.Paginate(queryDto).ToListAsync();

            var MappedWishList = _mapper.Map<IEnumerable<WishListGetDto>>(PaginateWishList);
            foreach (var item in MappedWishList)
            {
                var originalItem = PaginateWishList.FirstOrDefault(w=> w.PropertyID == item.PropertyID);
                if (originalItem != null)
                {
                    item.Price = originalItem.Property.Price;
                    item.Title = originalItem.Property.Address + "-" + originalItem.Property.City;
                }
            }

            var pagedResult = new PagedResultDto<WishListGetDto>(MappedWishList , queryDto.PageNumber , Count , queryDto.PageSize);


            return new ResponseDto<PagedResultDto<WishListGetDto?>>
            {
                IsSuccess = true,
                Message = "WishList retrieved successfully.",
                Data = pagedResult
            };
        }

        public async Task<ResponseDto<WishListGetDto?>> GetWishList(Guid CurrentUserId, Guid PropertyId)
        {
            if (CurrentUserId == Guid.Empty)
            {
                return new ResponseDto<WishListGetDto?>
                {
                    IsSuccess = false,
                    Message = "Invalid User ID.",
                    Data = null
                };
            }
            if (PropertyId == Guid.Empty)
            {
                return new  ResponseDto<WishListGetDto?>
                {
                    IsSuccess = false,
                    Message = "Invalid Listing ID.",
                    Data = null
                };
            }
            var wishlist = await _wishListRepository.GetWishList(CurrentUserId, PropertyId);
            if (wishlist == null)
            {
                return new ResponseDto<WishListGetDto?>
                {
                    IsSuccess = false,
                    Message = "No Item found for the given User ID and Property ID.",
                    Data = null
                };
            }
            var mappedWishList = _mapper.Map<WishListGetDto>(wishlist);
            mappedWishList.Price = wishlist.Property.Price;
            mappedWishList.Title = wishlist.Property.Address + "-" + wishlist.Property.City;
            return new ResponseDto<WishListGetDto?>
            {
                IsSuccess = true,
                Message = "Item retrieved successfully.",
                Data = mappedWishList
            };
        }
    }
}
