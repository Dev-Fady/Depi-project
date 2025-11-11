using DEPI_PROJECT.BLL.Dtos.Wishists;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IWishListService
    {
        public Task<ResponseDto<WishListGetDto?>> AddItemInWishList(Guid CurrentUserId,WishListAddDto wishlistDto);
        public Task<ResponseDto<bool>> DeleteItemInWishList(Guid CurrentUserId,  WishListDeleteDto wishlistDto);
        public Task<ResponseDto<PagedResultDto<WishListGetDto?>>> GetAllWishList(Guid CurrentUserId, WishListQueryDto queryDto);
        public Task<ResponseDto<WishListGetDto?>> GetWishList(Guid CurrentUserId, Guid PropertyId);
    }

}
