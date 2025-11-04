using DEPI_PROJECT.BLL.Dtos.Wishists;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IWishListService
    {
        public Task<bool> AddWishList(AddWishListDto wishlistDto);


        public Task<bool> DeleteWishList(DeleteWishListDto wishlistDto);

        public Task<IEnumerable<GetAllWishListDto>>? GetAllWishList(Guid UserId);

        public Task<bool> IsWishListFound(CheckWishListDto checkWishListDto);
    }
}

