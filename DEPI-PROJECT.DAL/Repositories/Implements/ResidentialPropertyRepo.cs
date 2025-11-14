using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            var query = context.ResidentialProperties
                .Include(x => x.Agent)
                .Include(x => x.Compound)
                .Include(x=>x.Amenity)
                .Include(x => x.PropertyGalleries);

            return query;
        }


        public async Task<ResidentialProperty?> GetResidentialPropertyByIdAsync(Guid id)
        {
            return await context.ResidentialProperties
                .Include(x=>x.Amenity)
                .Include(x => x.Agent)
                .Include(x => x.Compound)
                .Include(x => x.PropertyGalleries)
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

        public async Task AddAmenityAsync(Amenity amenity)
        {
            context.Amenities.Add(amenity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAmenityAsync(Amenity amenity)
        {
            var existing = context.Amenities.Find(amenity.PropertyId);
            if (existing == null) return;

            context.Entry(existing).CurrentValues.SetValues(amenity);
            await context.SaveChangesAsync();
        }
    }
}
