using DEPI_PROJECT.BLL.Dtos;
using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Manager.WishList
{
    public interface IWishListManger
    {
        public Task<bool> AddWishList(AddWishListDto wishlistDto);
        public Task<bool> DeleteWishList(DeleteWishListDto wishlistDto);
        public Task<IEnumerable<GetAllWishListDto>>? GetAllWishList(Guid UserId);
        public Task<bool> IsWishListFound(CheckWishListDto checkWishListDto);
    }

}
