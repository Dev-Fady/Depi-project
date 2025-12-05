using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        public IQueryable<CommercialProperty> GetAllProperties(Guid CurrentUserid)
        {
            var query = _context.CommercialProperties
                .Include(x => x.Agent)
                .Include(x => x.Compound)
                .Include(x => x.Amenity)
                .Include(x => x.Comments)
                .Include(x => x.PropertyGalleries)
                .Select(property => new CommercialProperty
                {
                    // Copy all properties from the original entity
                    PropertyId = property.PropertyId,
                    Title = property.Title,
                    City = property.City,
                    Address = property.Address,
                    GoogleMapsUrl = property.GoogleMapsUrl,
                    PropertyType = property.PropertyType,
                    PropertyPurpose = property.PropertyPurpose,
                    PropertyStatus = property.PropertyStatus,
                    Price = property.Price,
                    Square = property.Square,
                    Description = property.Description,
                    DateListed = property.DateListed,
                    AgentId = property.AgentId,
                    Agent = property.Agent,
                    CompoundId = property.CompoundId,
                    Compound = property.Compound,
                    
                    // Collections
                    Wishlists = property.Wishlists,
                    Comments = property.Comments,
                    Amenity = property.Amenity,
                    PropertyGalleries = property.PropertyGalleries,
                    LikeEntities = property.LikeEntities,
                    
                    // CommercialProperty specific properties
                    HasStorage = property.HasStorage,
                    FloorNumber = property.FloorNumber,
                    
                    // Likes data from view - use subqueries with automatic defaults
                    LikesCount = _context.PropertyLikesWithUserViews
                        .Where(v => v.PropertyId == property.PropertyId)
                        .Select(v => v.LikesCount)
                        .FirstOrDefault(), // Returns 0 if no match (int default)
                        
                    IsLiked = _context.LikeProperties
                        .Any(v => v.PropertyId == property.PropertyId && v.UserID == CurrentUserid)
                });
            return query;
        }

        public async Task<CommercialProperty?> GetPropertyByIdAsync(Guid CurrentUserid ,Guid id)
        {
            return await _context.CommercialProperties
                .Include(x => x.Agent)
                .Include(x => x.Compound)
                .Include(x => x.Amenity)
                .Include(x => x.Comments)
                .Include(x => x.PropertyGalleries)
                .Select(property => new CommercialProperty
                {
                    // Copy all properties from the original entity
                    PropertyId = property.PropertyId,
                    Title = property.Title,
                    City = property.City,
                    Address = property.Address,
                    GoogleMapsUrl = property.GoogleMapsUrl,
                    PropertyType = property.PropertyType,
                    PropertyPurpose = property.PropertyPurpose,
                    PropertyStatus = property.PropertyStatus,
                    Price = property.Price,
                    Square = property.Square,
                    Description = property.Description,
                    DateListed = property.DateListed,
                    AgentId = property.AgentId,
                    Agent = property.Agent,
                    CompoundId = property.CompoundId,
                    Compound = property.Compound,
                    
                    // Collections
                    Wishlists = property.Wishlists,
                    Comments = property.Comments,
                    Amenity = property.Amenity,
                    PropertyGalleries = property.PropertyGalleries,
                    LikeEntities = property.LikeEntities,
                    
                    // CommercialProperty specific properties
                    HasStorage = property.HasStorage,
                    FloorNumber = property.FloorNumber,
                    
                    // Likes data from view - use subqueries with automatic defaults
                    LikesCount = _context.PropertyLikesWithUserViews
                        .Where(v => v.PropertyId == property.PropertyId)
                        .Select(v => v.LikesCount)
                        .FirstOrDefault(), // Returns 0 if no match (int default)

                    IsLiked = _context.LikeProperties
                        .Any(v => v.PropertyId == property.PropertyId && v.UserID == CurrentUserid)
                })
                .FirstOrDefaultAsync(x => x.PropertyId == id);
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
