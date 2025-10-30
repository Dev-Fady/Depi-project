using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.DTOs.Response;
using DataModel = DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Manager.PropertyGallery
{
    public interface IPropertyGalleryManager
    {
        ResponseDto<IEnumerable<PropertyGalleryReadDto>> GetAll();
        ResponseDto<PropertyGalleryReadDto> GetById(Guid id);
        Task<ResponseDto<string>> Add(PropertyGalleryAddDto dto);
        ResponseDto<bool> Delete(Guid id);
        ResponseDto<IEnumerable<DataModel.PropertyGallery>> GetByPropertyId(Guid propertyId);
    }
}
