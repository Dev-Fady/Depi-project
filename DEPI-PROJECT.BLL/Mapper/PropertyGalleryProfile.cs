using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Mapper
{
    public class PropertyGalleryProfile : Profile
    {
        public PropertyGalleryProfile()
        {
            CreateMap<PropertyGallery, PropertyGalleryReadDto>();
            CreateMap<PropertyGalleryAddDto, PropertyGallery>();
            CreateMap<PropertyGalleryUpdateDto, PropertyGallery>();
        }
    }
}
