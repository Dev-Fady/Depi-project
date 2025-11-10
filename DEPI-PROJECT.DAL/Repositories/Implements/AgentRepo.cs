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
    public class AgentRepo : IAgentRepo
    {
        private readonly AppDbContext _context;

        public AgentRepo(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Agent> GetAll()
        {
            return _context.Agents
                            .Include(a => a.User)
                            .Include(a => a.Properties)
                                .ThenInclude(a => a.Compound)
                            .Include(a => a.Properties)
                                .ThenInclude(a => a.PropertyGalleries)
                            .Include(a => a.Properties)
                                .ThenInclude(a => a.Amenity)
                            .AsQueryable();
        }

        public async Task<Agent?> GetByIdAsync(Guid AgentId)
        {
            return await _context.Agents
                            .Include(a => a.User)
                            .Include(a => a.Properties)
                                .ThenInclude(a => a.Compound)
                            .Include(a => a.Properties)
                                .ThenInclude(a => a.PropertyGalleries)
                            .Include(a => a.Properties)
                                .ThenInclude(a => a.Amenity)
                            .FirstOrDefaultAsync(a => a.Id == AgentId);
                            
        }

        public async Task<Agent?> CreateAsync(Agent agent)
        {
            _context.Agents.Add(agent);  // Explicitly add to Agents DbSet
            int rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected == 0)
            {
                return null;
            }
            return await _context.Agents.FindAsync(agent.Id);  // Use the correct key
        }

        public async Task<bool> UpdateAsync(Agent agent)
        {
            _context.Update(agent);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid AgentId)
        {
            var agent = await _context.Agents.FirstOrDefaultAsync(a => a.Id == AgentId);
            if (agent == null)
            {
                return false;
            }
            _context.Remove(agent);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}