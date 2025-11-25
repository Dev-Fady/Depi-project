using AutoMapper;
using DEPI_PROJECT.BLL.Dtos.Comment;
using DEPI_PROJECT.BLL.Dtos.Wishists;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Exceptions;
using DEPI_PROJECT.BLL.Extensions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        private readonly IPropertyService _propertyService;

        public WishListService(IWishListRepository wishListRepository,
                                IMapper mapper,
                                IPropertyService propertyService) 
        {
            _wishListRepository = wishListRepository;
            _mapper = mapper;
            _propertyService = propertyService;
        }
        public async Task<ResponseDto<WishListGetDto?>> AddItemInWishList(Guid CurrentUserId, WishListAddDto wishlistDto)
        {
            if (CurrentUserId == Guid.Empty || wishlistDto.PropertyID == Guid.Empty)
            {
                throw new BadRequestException("User id and property Id both cannot be null");
            }

            if(await _propertyService.CheckPropertyExist(wishlistDto.PropertyID) == false)
            {
                throw new NotFoundException($"No property found with Id {wishlistDto.PropertyID}");
            }

            //check if already exists
            var exists = await _wishListRepository.GetWishList(CurrentUserId, wishlistDto.PropertyID);
            if (exists != null)
            {
                throw new InvalidOperationException("This Item is already in your wishlist.");
            }

            var wishlist = _mapper.Map<Wishlist>(wishlistDto);
            wishlist.UserID = CurrentUserId;

            var Result = await _wishListRepository.AddItemInWishList(wishlist);
            if (!Result)
            {
                throw new Exception("An error occurred while adding the item to the wishlist, please try again");
            }

            //not include property details in mapping so manually adding them
            var fullWishlist = await _wishListRepository.GetWishList(wishlist.UserID, wishlist.PropertyID);

            if(fullWishlist == null)
            {
                throw new NotFoundException($"No wishlist Found for user ID {wishlist.UserID}");
            }

            var mappedWishList = _mapper.Map<WishListGetDto>(fullWishlist);
            mappedWishList.Price = fullWishlist.Property.Price;
            mappedWishList.Title = fullWishlist.Property.Title;
            
            return new ResponseDto<WishListGetDto?>
            {
                IsSuccess = true,
                Message = "Item added successfully.",
                Data = mappedWishList
            };
        }

        public async Task<ResponseDto<bool>> DeleteItemInWishList(Guid CurrentUserId, WishListDeleteDto wishlistDto)
        {
            if (CurrentUserId == Guid.Empty || wishlistDto.ListingID == Guid.Empty)
            {
                throw new BadRequestException("User id and item Id both cannot be null");
            }
            bool result =  await _wishListRepository.DeleteItemInWishList(CurrentUserId, wishlistDto.ListingID);
            if (!result)
            {
                throw new Exception("An error occurred while removing the item from the wishlist, please try again");

            }
            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "Successfuly deleted Item.",
                Data = true
            };
        }

        public async Task<ResponseDto<PagedResultDto<WishListGetDto>>> GetAllWishList(Guid CurrentUserId, WishListQueryDto queryDto)
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
                    item.Title = originalItem.Property.Title;
                }
            }

            var pagedResult = new PagedResultDto<WishListGetDto>(MappedWishList , queryDto.PageNumber , Count , queryDto.PageSize);


            return new ResponseDto<PagedResultDto<WishListGetDto>>
            {
                IsSuccess = true,
                Message = "WishList retrieved successfully.",
                Data = pagedResult
            };
        }

        public async Task<ResponseDto<WishListGetDto?>> GetWishList(Guid CurrentUserId, Guid PropertyId)
        {
            if (CurrentUserId == Guid.Empty || PropertyId == Guid.Empty)
            {
                throw new BadRequestException("User id and property Id both cannot be null");
            }

            if(await _propertyService.CheckPropertyExist(PropertyId) == false)
            {
                throw new NotFoundException($"No property found with Id {PropertyId}");
            }

            var wishlist = await _wishListRepository.GetWishList(CurrentUserId, PropertyId);
            if (wishlist == null)
            {
                throw new NotFoundException($"No Item found for the given User {CurrentUserId} and Property {PropertyId}.");
            }
            var mappedWishList = _mapper.Map<WishListGetDto>(wishlist);
            mappedWishList.Price = wishlist.Property.Price;
            mappedWishList.Title = wishlist.Property.Title;
            return new ResponseDto<WishListGetDto?>
            {
                IsSuccess = true,
                Message = "Item retrieved successfully.",
                Data = mappedWishList
            };
        }
    }
}
