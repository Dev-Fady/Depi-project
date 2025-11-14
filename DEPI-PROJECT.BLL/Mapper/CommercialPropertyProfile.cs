using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.DAL.Models;


namespace DEPI_PROJECT.BLL.Mapper
{
    public class CommercialPropertyProfile : Profile
    {
        public CommercialPropertyProfile()
        {
            CreateMap<CommercialProperty, CommercialPropertyReadDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Agent.UserId))
                .ForMember(dest => dest.Galleries, opt => opt.MapFrom(src => src.PropertyGalleries ?? new List<PropertyGallery>()))
                .ForMember(dest => dest.Amenity, opt => opt.MapFrom(src => src.Amenity != null ? src.Amenity : null));

            CreateMap<CommercialProperty, AllPropertyReadDto>()
                .ForMember(dest => dest.ResidentialProperties, opt => opt.Ignore());

            CreateMap<CommercialPropertyAddDto, CommercialProperty>()
                .ForMember(dest => dest.Amenity, opt => opt.Ignore())
                .ForMember(dest => dest.Agent, opt => opt.Ignore())
                .ForMember(dest => dest.Compound, opt => opt.Ignore())
                .ForMember(dest => dest.PropertyGalleries, opt => opt.Ignore());

            CreateMap<CommercialPropertyUpdateDto, CommercialProperty>()
                .ForMember(dest => dest.PropertyId, opt => opt.Ignore())
                .ForMember(dest => dest.Amenity, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}
