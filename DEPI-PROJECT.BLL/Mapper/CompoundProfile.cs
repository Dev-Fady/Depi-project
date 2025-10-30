using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Compound;
using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class CompoundProfile : Profile
    {
        public CompoundProfile()
        {
            CreateMap<Compound, CompoundReadDto>();
            CreateMap<CompoundAddDto, Compound>();
            CreateMap<CompoundUpdateDto, Compound>()
                .ForMember(dest => dest.CompoundId, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
