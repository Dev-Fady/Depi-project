using DEPI_PROJECT.BLL.DTOs.Pagination;
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

        public PagedResult<ResidentialProperty> GetAllResidentialProperty(int pageNumber, int pageSize)
        {
            var query = context.ResidentialProperties
                .Include(x => x.Agent)
                .Include(x => x.Compound)
                .Include(x=>x.Amenity)
                .Include(x => x.PropertyGalleries);

            var totalCount = query.Count();

            var data = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<ResidentialProperty>
            {
                Data = data,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }


        public ResidentialProperty? GetResidentialPropertyById(Guid id)
        {
            return context.ResidentialProperties
                .Include(x=>x.Amenity)
                .FirstOrDefault(rp => rp.PropertyId == id);
        }
        public void AddResidentialProperty(ResidentialProperty property)
        {
            context.ResidentialProperties.Add(property);
            context.SaveChanges();
        }
        public void DeleteResidentialProperty(Guid id)
        {
            var property = context.ResidentialProperties.Find(id);
            if (property == null) return;

            context.ResidentialProperties.Remove(property);
            context.SaveChanges();
        }

        public void UpdateResidentialProperty(Guid id, ResidentialProperty property)
        {
            var existing = context.ResidentialProperties.Find(id);
            if (existing == null) return;

            context.Entry(existing).CurrentValues.SetValues(property);
            context.SaveChanges();
        }

        public void AddAmenity(Amenity amenity)
        {
            context.Amenities.Add(amenity);
            context.SaveChanges();
        }

        public void UpdateAmenity(Amenity amenity)
        {
            var existing = context.Amenities.Find(amenity.PropertyId);
            if (existing == null) return;

            context.Entry(existing).CurrentValues.SetValues(amenity);
            context.SaveChanges();
        }
    }
}
