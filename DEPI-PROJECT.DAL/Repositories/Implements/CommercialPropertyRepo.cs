using DEPI_PROJECT.BLL.DTOs.Pagination;
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
        public PagedResult<CommercialProperty> GetAllProperties(int pageNumber, int pageSize)
        {
            var query = _context.CommercialProperties
                 .Include(x => x.Agent)
                .Include(x => x.Compound)
                .Include(x => x.Amenity)
                .Include(x => x.PropertyGalleries);

            var totalCount=query.Count();
            var data = query
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .ToList();
            return new PagedResult<CommercialProperty>
            {
                Data = data,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        public CommercialProperty? GetPropertyById(Guid id)
        {
            return _context.CommercialProperties
                 .Include(p => p.Amenity)
                    .Include(p => p.PropertyGalleries)
                    .Include(p => p.Agent)
                    .Include(p => p.Compound)
                   .FirstOrDefault(x => x.PropertyId == id);
        }

        public void UpdateAmenity(Amenity amenity)
        {
            var existing =  _context.CommercialProperties.Find(amenity.PropertyId);
            if (existing == null) {
                return;
            }
            _context.Entry(existing).CurrentValues.SetValues(amenity);
            _context.SaveChanges();
        }

        public void UpdateCommercialProperty(Guid id, CommercialProperty property)
        {
            var existing = _context.CommercialProperties
                           .Include(p => p.Amenity)
                           .FirstOrDefault(p => p.PropertyId == id);

            if (existing == null)
                return;

            _context.Entry(existing).CurrentValues.SetValues(property);
            _context.Entry(existing).Property(e => e.PropertyId).IsModified = false;
            _context.SaveChanges();
        }
        public void AddAmenity(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            _context.SaveChanges();
        }

        public void AddCommercialProperty(CommercialProperty property)
        {
            _context.CommercialProperties.Add(property);
            _context.SaveChanges();
        }

        public void DeleteCommercialProperty(Guid id)
        {
            var property = _context.CommercialProperties.Find(id);
            if (property == null) return;

            _context.CommercialProperties.Remove(property);
            _context.SaveChanges();
        }

    }
}
