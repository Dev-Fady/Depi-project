using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthRegisterDto, Agent>();

            CreateMap<AuthRegisterDto, Broker>();

            CreateMap<AuthRegisterDto, User>()
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
            .AfterMap((src, dest, context) => {
                if (src.RoleDiscriminator == UserRoleOptions.Agent)
                {
                    dest.Agent = new Agent();
                    dest.Agent.AgencyName = src.AgencyName ?? "N/A";
                    dest.Agent.ExperienceYears = src.experienceYears ?? 0;
                    dest.Agent.TaxIdentificationNumber = src.TaxIdentificationNumber ?? 0;
                }
                else if (src.RoleDiscriminator == UserRoleOptions.Broker)
                {
                    dest.Broker = new Broker();
                    dest.Broker.LicenseID = src.LicenseID ?? "N/A";
                    dest.Broker.NationalID = src.NationalID ?? "N/A";
                }
            });
        }
    }
}