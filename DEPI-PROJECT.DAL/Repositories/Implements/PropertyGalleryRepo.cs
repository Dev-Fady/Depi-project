using System.Threading.Tasks;
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

        public async Task AddAsync(PropertyGallery gallery)
        {
            if (gallery == null)
                throw new ArgumentNullException(nameof(gallery));

            _context.PropertyGalleries.Add(gallery);
            var affected = await _context.SaveChangesAsync();
            Console.WriteLine($"Rows affected: {affected}");
        }

        public async Task DeleteAsync(Guid id)
        {
            var gallery = await _context.PropertyGalleries.FirstOrDefaultAsync(g => g.MediaId == id);
            if (gallery == null)
                throw new KeyNotFoundException($"No gallery found with Id = {id}");

            _context.PropertyGalleries.Remove(gallery);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PropertyGallery>> GetAllAsync()
        {
            return await _context.PropertyGalleries
                           .AsNoTracking()
                           .ToListAsync();
        }

        public async Task<IEnumerable<PropertyGallery>> GetByPropertyIdAsync(Guid propertyId)
        {
            return await _context.PropertyGalleries
                           .AsNoTracking()
                           .Where(g => g.PropertyId == propertyId)
                           .ToListAsync();
        }
        public async Task AddRangeAsync(IEnumerable<PropertyGallery> galleries)
        {
            _context.PropertyGalleries.AddRange(galleries);
            var affected = await _context.SaveChangesAsync();
            Console.WriteLine($"Rows affected: {affected}");
        }
        public async Task<PropertyGallery?> GetByIdAsync(Guid id)
        {
            return await _context.PropertyGalleries
                            .Include(g => g.Property)
                                .ThenInclude(p => p.Agent)
                           //.AsNoTracking()
                           .FirstOrDefaultAsync(g => g.MediaId == id);
        }
    }
}
