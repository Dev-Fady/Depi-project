using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.DTOs.Response;
using DataModel = DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IPropertyGalleryService
    {
        Task<ResponseDto<IEnumerable<PropertyGalleryReadDto>>> GetAllAsync();
        Task<ResponseDto<PropertyGalleryReadDto>> GetByIdAsync(Guid id);
        Task<ResponseDto<string>> AddAsync(Guid UserId, PropertyGalleryAddDto dto);
        Task<ResponseDto<bool>> DeleteAsync(Guid UserId, Guid id);
        Task<ResponseDto<IEnumerable<PropertyGalleryReadDto>>> GetByPropertyIdAsync(Guid propertyId);
    }
}
