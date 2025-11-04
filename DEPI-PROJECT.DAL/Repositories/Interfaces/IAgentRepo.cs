using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface IAgentRepo
    {
        public IQueryable<Agent> GetAll();
        public Task<Agent?> GetByIdAsync(Guid AgentId);
        public Task<Agent?> CreateAsync(Agent agent);
        public Task<bool> UpdateAsync(Agent agent);
        public Task<bool> DeleteAsync(Guid AgentId);
    }
}