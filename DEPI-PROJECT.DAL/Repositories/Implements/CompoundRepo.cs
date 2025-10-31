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
      
        public IQueryable<Compound> GetAllCompounds()
        {
            var query = _context.Compounds;
            return query;
        }

        public async Task<Compound?> GetCompoundByIdAsync(Guid id)
        {
            return await _context.Compounds
                .FirstOrDefaultAsync(x => x.CompoundId == id);
        }

        public async Task UpdateCompoundAsync(Guid id, Compound compound)
        {
            var existing = await _context.Compounds.FindAsync(id);
            if (existing == null)
            {
                return;
            }
            _context.Entry(existing).CurrentValues.SetValues(compound);
            await _context.SaveChangesAsync();
        }
        public async Task AddCompoundAsync(Compound compound)
        {
            _context.Compounds.Add(compound);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompoundAsync(Guid id)
        {
            var compound = await _context.Compounds.FindAsync(id);
            if (compound == null)
            {
                return;
            }
            _context.Compounds.Remove(compound);
            await _context.SaveChangesAsync();
        }

    }
}
