using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Amenity;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class AmenityProfile : Profile
    {
        public AmenityProfile()
        {
            CreateMap<Amenity, AmenityReadDto>();

            CreateMap<AmenityAddDto, Amenity>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));    
        }
    }
}