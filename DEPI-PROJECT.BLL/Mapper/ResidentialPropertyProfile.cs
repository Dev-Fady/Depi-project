using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.DAL.Models;
using System.Linq;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class ResidentialPropertyProfile : Profile
    {
        public ResidentialPropertyProfile()
        {
            CreateMap<ResidentialProperty, ResidentialPropertyReadDto>()
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent.Name))
                .ForMember(dest => dest.CompoundName, opt => opt.MapFrom(src => src.Compound.Name))
                .ForMember(dest => dest.GalleryUrls, opt => opt.MapFrom(src => src.PropertyGalleries.Select(g => g.ImageUrl)));

            CreateMap<ResidentialPropertyAddDto, ResidentialProperty>();

            CreateMap<ResidentialPropertyUpdateDto, ResidentialProperty>();

            CreateMap<ResidentialPropertyUpdateDto, ResidentialProperty>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


        }
    }
}
