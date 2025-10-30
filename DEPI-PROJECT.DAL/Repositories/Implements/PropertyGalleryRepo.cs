using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using DataModels = DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.DAL.Repositories.Implements
{
    public class PropertyGalleryRepo : IPropertyGalleryRepo
    {
        private readonly AppDbContext _context;
        public PropertyGalleryRepo(AppDbContext context)
        {
            _context = context;
        }

        public void Add(PropertyGallery gallery)
        {
            if (gallery == null)
                throw new ArgumentNullException(nameof(gallery));

            _context.PropertyGalleries.Add(gallery);
            var affected = _context.SaveChanges();
            Console.WriteLine($"Rows affected: {affected}");
        }

        public void Delete(Guid id)
        {
            var gallery = _context.PropertyGalleries.FirstOrDefault(g => g.MediaId == id);
            if (gallery == null)
                throw new KeyNotFoundException($"No gallery found with Id = {id}");

            _context.PropertyGalleries.Remove(gallery);
            _context.SaveChanges();
        }

        public IEnumerable<PropertyGallery> GetAll()
        {
            return _context.PropertyGalleries
                           .AsNoTracking()
                           .ToList();
        }

        public IEnumerable<PropertyGallery> GetByPropertyId(Guid propertyId)
        {
            return _context.PropertyGalleries
                           .AsNoTracking()
                           .Where(g => g.PropertyId == propertyId)
                           .ToList();
        }
        public void AddRange(IEnumerable<PropertyGallery> galleries)
        {
            _context.PropertyGalleries.AddRange(galleries);
            var affected = _context.SaveChanges();
            Console.WriteLine($"Rows affected: {affected}");
        }
        public PropertyGallery? GetById(Guid id)
        {
            return _context.PropertyGalleries
                           //.AsNoTracking()
                           .FirstOrDefault(g => g.MediaId == id);
        }
    }
}
