using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class PropertyProfile : Profile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyResponseDto>();
        }
    }
}
