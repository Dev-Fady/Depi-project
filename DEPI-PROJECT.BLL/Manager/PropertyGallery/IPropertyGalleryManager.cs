using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DataModel = DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Manager.PropertyGallery
{
    public interface IPropertyGalleryManager
    {
        IEnumerable<PropertyGalleryReadDto> GetAll();
        PropertyGalleryReadDto GetById(Guid id);
        Task Add(PropertyGalleryAddDto dto);
        bool Delete(Guid id);
        IEnumerable<DataModel.PropertyGallery> GetByPropertyId(Guid propertyId);
    }
}
