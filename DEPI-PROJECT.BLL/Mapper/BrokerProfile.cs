using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Broker;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class BrokerProfile : Profile
    {
        public BrokerProfile()
        {
            CreateMap<Broker, BrokerResponseDto>();

            CreateMap<BrokerUpdateDto, Broker>()
            .ForMember(dest => dest.LicenseID, opt => opt.Condition(src => src.LicenseID != null))
            .ForMember(dest => dest.NationalID, opt => opt.Condition(src => src.NationalID != null));

            CreateMap<BrokerCreateDto, Broker>();
        }

    }

}