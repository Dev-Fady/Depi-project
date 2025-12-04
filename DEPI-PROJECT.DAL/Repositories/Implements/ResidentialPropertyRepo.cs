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
        private readonly AppDbContext context;

        public ResidentialPropertyRepo(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ResidentialProperty> GetAllResidentialProperty()
        {
            // Use separate subqueries for likes data - most reliable approach
            var query = context.ResidentialProperties
                .Include(x => x.Agent)
                .Include(x => x.Compound)
                .Include(x => x.Amenity)
                .Include(x => x.Comments)
                .Include(x => x.PropertyGalleries)
                .Select(property => new ResidentialProperty
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
                    
                    // ResidentialProperty specific properties
                    Bedrooms = property.Bedrooms,
                    Bathrooms = property.Bathrooms,
                    Floors = property.Floors,
                    KitchenType = property.KitchenType,
                    
                    // Likes data from view - use subqueries with automatic defaults
                    LikesCount = context.PropertyLikesWithUserViews
                        .Where(v => v.PropertyId == property.PropertyId)
                        .Select(v => v.LikesCount)
                        .FirstOrDefault(), // Returns 0 if no match (int default)
                        
                    IsLiked = context.PropertyLikesWithUserViews
                        .Where(v => v.PropertyId == property.PropertyId)
                        .Select(v => v.IsLiked)
                        .FirstOrDefault() // Returns false if no match (bool default)
                });

            return query;
        }


        public async Task<ResidentialProperty?> GetResidentialPropertyByIdAsync(Guid id)
        {
            return await context.ResidentialProperties
                .Include(x=>x.Amenity)
                .Include(x => x.Agent)
                .Include(x => x.Compound)
                .Include(x => x.PropertyGalleries)
                .Include(x => x.Comments)
                .Select(property => new ResidentialProperty
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
                    
                    // ResidentialProperty specific properties
                    Bedrooms = property.Bedrooms,
                    Bathrooms = property.Bathrooms,
                    Floors = property.Floors,
                    KitchenType = property.KitchenType,
                    
                    // Likes data from view - use subqueries with automatic defaults
                    LikesCount = context.PropertyLikesWithUserViews
                        .Where(v => v.PropertyId == property.PropertyId)
                        .Select(v => v.LikesCount)
                        .FirstOrDefault(), // Returns 0 if no match (int default)
                        
                    IsLiked = context.PropertyLikesWithUserViews
                        .Where(v => v.PropertyId == property.PropertyId)
                        .Select(v => v.IsLiked)
                        .FirstOrDefault() // Returns false if no match (bool default)
                })
                .FirstOrDefaultAsync(rp => rp.PropertyId == id);
        }
        public async Task AddResidentialPropertyAsync(ResidentialProperty property)
        {
            context.ResidentialProperties.Add(property);
            await context.SaveChangesAsync();
        }
        public async Task DeleteResidentialPropertyAsync(Guid id)
        {
            var property = context.ResidentialProperties.Find(id);
            if (property == null) return;

            context.ResidentialProperties.Remove(property);
            await context.SaveChangesAsync();
        }

        public async Task UpdateResidentialPropertyAsync(Guid id, ResidentialProperty property)
        {
            var existing = context.ResidentialProperties.Find(id);
            if (existing == null) return;

            context.Entry(existing).CurrentValues.SetValues(property);
            await context.SaveChangesAsync();
        }
    }
}
