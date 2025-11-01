using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class AgentProfile : Profile
    {
        public AgentProfile()
        {
            CreateMap<Agent, AgentResponseDto>();

            CreateMap<AgentUpdateDto, Agent>()
            .ForMember(dest => dest.AgencyName, opt => opt.Condition(src => src.AgencyName != null))
            .ForMember(dest => dest.TaxIdentificationNumber, opt => opt.Condition(src => src.TaxIdentificationNumber != null))
            .ForMember(dest => dest.ExperienceYears, opt => opt.Condition(src => src.experienceYears != null))
            .ForMember(dest => dest.Rating, opt => opt.Condition(src => src.Rating != null));

            CreateMap<AgentCreateDto, Agent>();
        }

    }

}