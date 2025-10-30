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
    public class CompoundRepo : ICompoundRepo
    {
        private readonly AppDbContext _context;

        public CompoundRepo(AppDbContext context)
        {
            _context = context;
        }
      
        public PagedResult<Compound> GetAllCompounds(int pageNumber, int pageSize)
        {
            var query = _context.Compounds;
            var totalCount = query.Count();
            var data = query
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .ToList();
            return new PagedResult<Compound>
            {
                Data = data,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }

        public Compound? GetCompoundById(Guid id)
        {
            return _context.Compounds
                .FirstOrDefault(x => x.CompoundId == id);
        }

        public void UpdateCompound(Guid id, Compound compound)
        {
            var existing = _context.Compounds.Find(id);
            if (existing == null)
            {
                return;
            }
            _context.Entry(existing).CurrentValues.SetValues(compound);
            _context.SaveChanges();
        }
        public void AddCompound(Compound compound)
        {
            _context.Compounds.Add(compound);
            _context.SaveChanges();
        }

        public void DeleteCompound(Guid id)
        {
            var compound = _context.Compounds.Find(id);
            if (compound == null)
            {
                return;
            }
            _context.Compounds.Remove(compound);
            _context.SaveChanges();
        }

    }
}
