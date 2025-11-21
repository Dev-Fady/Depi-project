using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface IBrokerRepo
    {
        public IQueryable<Broker> GetAll();
        public Task<Broker?> GetByIdAsync(Guid UserId);
        public Task<Broker?> CreateAsync(Broker Broker);
        public Task<bool> UpdateAsync(Broker Broker);
        public Task<bool> DeleteAsync(Guid UserId);
    }
}