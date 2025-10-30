using Microsoft.EntityFrameworkCore;
using DataModels = DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface IPropertyGalleryRepo
    {
        IEnumerable<DataModels.PropertyGallery> GetAll();
        IEnumerable<DataModels.PropertyGallery> GetByPropertyId(Guid propertyId);
        void Add(DataModels.PropertyGallery gallery);
        void Delete(Guid id);
        DataModels.PropertyGallery GetById(Guid id);
        void AddRange(IEnumerable<DataModels.PropertyGallery> galleries);
    }
}
