using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class ResidentialPropertyProfile : Profile
    {
        public ResidentialPropertyProfile()
        {
            CreateMap<ResidentialProperty, ResidentialPropertyReadDto>()
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent != null ? src.Agent.Name : null))
                .ForMember(dest => dest.CompoundName, opt => opt.MapFrom(src => src.Compound != null ? src.Compound.Name : null))
                .ForMember(dest => dest.Galleries, opt => opt.MapFrom(src => src.PropertyGalleries))
                .ForMember(dest => dest.Amenity, opt => opt.MapFrom(src => src.Amenity)); 

            CreateMap<Amenity, AmenityReadDto>();

            CreateMap<ResidentialPropertyAddDto, ResidentialProperty>()
                .ForMember(dest => dest.Amenity, opt => opt.Ignore()) 
                .ForMember(dest => dest.Agent, opt => opt.Ignore())
                .ForMember(dest => dest.Compound, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyGalleries, opt => opt.Ignore())
                .ForMember(dest => dest.Wishlists, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore());

            CreateMap<AmenityAddDto, Amenity>();

            CreateMap<ResidentialPropertyUpdateDto, ResidentialProperty>()
                .ForMember(dest => dest.PropertyId, opt => opt.Ignore()) 
                .ForMember(dest => dest.Amenity, opt => opt.Ignore()) 
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<AmenityAddDto, Amenity>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
