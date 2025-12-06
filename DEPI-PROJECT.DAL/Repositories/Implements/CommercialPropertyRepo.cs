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
                .GroupJoin(
                    _context.PropertyLikesWithUserViews,
                    p => p.PropertyId,
                    lk => lk.PropertyId,
                    (property, likes) => new { property, likesData = likes.DefaultIfEmpty().FirstOrDefault() }
                )
                .Select(x => new CommercialProperty
                {
                    // Copy all properties from the original entity
                    PropertyId = x.property.PropertyId,
                    Title = x.property.Title,
                    City = x.property.City,
                    Address = x.property.Address,
                    GoogleMapsUrl = x.property.GoogleMapsUrl,
                    PropertyType = x.property.PropertyType,
                    PropertyPurpose = x.property.PropertyPurpose,
                    PropertyStatus = x.property.PropertyStatus,
                    Price = x.property.Price,
                    Square = x.property.Square,
                    Description = x.property.Description,
                    DateListed = x.property.DateListed,
                    AgentId = x.property.AgentId,
                    Agent = x.property.Agent,
                    CompoundId = x.property.CompoundId,
                    Compound = x.property.Compound,
                    
                    // Collections
                    Wishlists = x.property.Wishlists,
                    Comments = x.property.Comments,
                    Amenity = x.property.Amenity,
                    PropertyGalleries = x.property.PropertyGalleries,
                    LikeEntities = x.property.LikeEntities,
                    
                    // CommercialProperty specific properties
                    HasStorage = x.property.HasStorage,
                    FloorNumber = x.property.FloorNumber,
                    
                    // Likes data from view - check PropertyId instead of entity for null
                    LikesCount = x.likesData.LikesCount ?? 0,
                    IsLiked = false // will be set in the service
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
                .GroupJoin(
                    _context.PropertyLikesWithUserViews,
                    p => p.PropertyId,
                    lk => lk.PropertyId,
                    (property, likes) => new { property, likesData = likes.DefaultIfEmpty().FirstOrDefault() }
                )
                .Select(x => new CommercialProperty
                {
                    // Copy all properties from the original entity
                    PropertyId = x.property.PropertyId,
                    Title = x.property.Title,
                    City = x.property.City,
                    Address = x.property.Address,
                    GoogleMapsUrl = x.property.GoogleMapsUrl,
                    PropertyType = x.property.PropertyType,
                    PropertyPurpose = x.property.PropertyPurpose,
                    PropertyStatus = x.property.PropertyStatus,
                    Price = x.property.Price,
                    Square = x.property.Square,
                    Description = x.property.Description,
                    DateListed = x.property.DateListed,
                    AgentId = x.property.AgentId,
                    Agent = x.property.Agent,
                    CompoundId = x.property.CompoundId,
                    Compound = x.property.Compound,
                    
                    // Collections
                    Wishlists = x.property.Wishlists,
                    Comments = x.property.Comments,
                    Amenity = x.property.Amenity,
                    PropertyGalleries = x.property.PropertyGalleries,
                    LikeEntities = x.property.LikeEntities,
                    
                    // CommercialProperty specific properties
                    HasStorage = x.property.HasStorage,
                    FloorNumber = x.property.FloorNumber,
                    
                    // Likes data from view - check PropertyId instead of entity for null
                    LikesCount = x.likesData.LikesCount ?? 0,
                    IsLiked = false // will be set in the service
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
