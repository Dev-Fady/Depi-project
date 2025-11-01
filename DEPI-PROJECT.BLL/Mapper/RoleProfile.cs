using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Role;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleResponseDto>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Id));

            CreateMap<RoleUpdateDto, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RoleId));

            CreateMap<RoleCreateDto, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleName));
        }

    }

}