using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DEPI_PROJECT.DAL.Repositories.Implements
{
    public class CommercialPropertyRepo : ICommercialPropertyRepo
    {
        private readonly AppDbContext _context;

        public CommercialPropertyRepo(AppDbContext context)
        {
            _context = context;
        }
        public IQueryable<CommercialProperty> GetAllProperties()
        {
            var query = _context.CommercialProperties
                 .Include(x => x.Agent)
                .Include(x => x.Compound)
                .Include(x => x.Amenity)
                .Include(x => x.PropertyGalleries);

            return query;
        }

        public async Task<CommercialProperty?> GetPropertyByIdAsync(Guid id)
        {
            return await _context.CommercialProperties
                                .Include(p => p.Amenity)
                                .Include(p => p.PropertyGalleries)
                                .Include(p => p.Agent)
                                .Include(p => p.Compound)
                                .FirstOrDefaultAsync(x => x.PropertyId == id);
        }

        public async Task UpdateAmenityAsync(Amenity amenity)
        {
            var existing =  await _context.CommercialProperties.FindAsync(amenity.PropertyId);
            if (existing == null) {
                return;
            }
            _context.Entry(existing).CurrentValues.SetValues(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommercialPropertyAsync(Guid id, CommercialProperty property)
        {
            var existing = await _context.CommercialProperties
                                        .Include(p => p.Amenity)
                                        .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (existing == null)
                return;

            _context.Entry(existing).CurrentValues.SetValues(property);
            _context.Entry(existing).Property(e => e.PropertyId).IsModified = false;
            await _context.SaveChangesAsync();
        }
        public async Task AddAmenityAsync(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task AddCommercialPropertyAsync(CommercialProperty property)
        {
            _context.CommercialProperties.Add(property);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommercialPropertyAsync(Guid id)
        {
            var property = await _context.CommercialProperties.FindAsync(id);
            if (property == null) return;

            _context.CommercialProperties.Remove(property);
            await _context.SaveChangesAsync();
        }

    }
}
