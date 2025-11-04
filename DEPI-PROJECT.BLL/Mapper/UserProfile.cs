using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDto>();

            CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.Condition(src => src.Username != null))
            .ForMember(dest => dest.PhoneNumber, opt => opt.Condition(src => src.PhoneNumber != null))
            .ForMember(dest => dest.Email, opt => opt.Condition(src => src.Email != null));
        }
    }
}