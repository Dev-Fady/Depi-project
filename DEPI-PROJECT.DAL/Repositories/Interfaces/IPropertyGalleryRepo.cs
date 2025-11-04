using DEPI_PROJECT.DAL.Models;
using Microsoft.EntityFrameworkCore;
using DataModels = DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface IPropertyGalleryRepo
    {
        Task<IEnumerable<PropertyGallery>> GetAllAsync();
        Task<IEnumerable<PropertyGallery>> GetByPropertyIdAsync(Guid propertyId);
        Task AddAsync(PropertyGallery gallery);
        Task DeleteAsync(Guid id);
        Task<PropertyGallery?> GetByIdAsync(Guid id);
        Task AddRangeAsync(IEnumerable<PropertyGallery> galleries);
    }
}
