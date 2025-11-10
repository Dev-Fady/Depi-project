using AutoMapper;
using DEPI_PROJECT.BLL.Dtos.Wishists;
using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class WishListProfile : Profile
    {
        public WishListProfile()
        {
            CreateMap<WishListAddDto,Wishlist>()
                .ForMember(dest => dest.UserID, opt => opt.Ignore());
            CreateMap<Wishlist, WishListGetDto>();
            CreateMap<WishListDeleteDto,Wishlist >();

        }
    }
}
