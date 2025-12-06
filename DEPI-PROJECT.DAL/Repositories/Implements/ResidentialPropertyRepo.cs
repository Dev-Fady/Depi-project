using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Repositories.Implements
{
    public class ResidentialPropertyRepo : IResidentialPropertyRepo
    {
        private readonly AppDbContext _context;

        public ResidentialPropertyRepo(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<ResidentialProperty> GetAllResidentialProperty(Guid CurrentUserid)
        {
            // Use separate subqueries for likes data - most reliable approach
            var query = _context.ResidentialProperties
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
                .Select(x => new ResidentialProperty
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
                    
                    // ResidentialProperty specific properties
                    Bedrooms = x.property.Bedrooms,
                    Bathrooms = x.property.Bathrooms,
                    Floors = x.property.Floors,
                    KitchenType = x.property.KitchenType,
                    
                    // Likes data - translates to ISNULL(v.LikesCount, 0) in SQL
                    LikesCount = x.likesData.LikesCount ?? 0,
                    IsLiked = false
                });
                
            return query;
        }


        public async Task<ResidentialProperty?> GetResidentialPropertyByIdAsync(Guid CurrentUserid , Guid id)
        {
            return await _context.ResidentialProperties
                .Include(x=>x.Amenity)
                .Include(x => x.Agent)
                .Include(x => x.Compound)
                .Include(x => x.PropertyGalleries)
                .Include(x => x.Comments)
                .GroupJoin(
                    _context.PropertyLikesWithUserViews,
                    p => p.PropertyId,
                    lk => lk.PropertyId,
                    (property, likes) => new { property, likesData = likes.DefaultIfEmpty().FirstOrDefault() }
                )
                .Select(x => new ResidentialProperty
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
                    
                    // ResidentialProperty specific properties
                    Bedrooms = x.property.Bedrooms,
                    Bathrooms = x.property.Bathrooms,
                    Floors = x.property.Floors,
                    KitchenType = x.property.KitchenType,
                    
                    // Likes data - translates to ISNULL(v.LikesCount, 0) in SQL
                    LikesCount = x.likesData.LikesCount ?? 0,
                    IsLiked = false
                })
                .FirstOrDefaultAsync(rp => rp.PropertyId == id);
        }
        public async Task AddResidentialPropertyAsync(ResidentialProperty property)
        {
            _context.ResidentialProperties.Add(property);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteResidentialPropertyAsync(Guid id)
        {
            var property = _context.ResidentialProperties.Find(id);
            if (property == null) return;

            _context.ResidentialProperties.Remove(property);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateResidentialPropertyAsync(Guid id, ResidentialProperty property)
        {
            var existing = _context.ResidentialProperties.Find(id);
            if (existing == null) return;

            _context.Entry(existing).CurrentValues.SetValues(property);
            await _context.SaveChangesAsync();
        }
    }
}
