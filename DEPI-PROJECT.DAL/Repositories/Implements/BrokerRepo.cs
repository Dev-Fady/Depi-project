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
    public class BrokerRepo : IBrokerRepo
    {
        private readonly AppDbContext _context;

        public BrokerRepo(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Broker> GetAll()
        {
            return _context.Brokers
                            .Include(a => a.User)
                            .AsQueryable();
        }

        public async Task<Broker?> GetByIdAsync(Guid BrokerId)
        {
            return await _context.Brokers
                            .Include(a => a.User)
                            .FirstOrDefaultAsync(a => a.Id == BrokerId);
                            
        }

        public async Task<Broker?> CreateAsync(Broker Broker)
        {
            _context.Brokers.Add(Broker);  // Explicitly add to Brokers DbSet
            int rowsAffected = await _context.SaveChangesAsync();
            if(rowsAffected == 0)
            {
                return null;
            }
            return await _context.Brokers.FindAsync(Broker.Id);  // Use the correct key
        }

        public async Task<bool> UpdateAsync(Broker Broker)
        {
            _context.Update(Broker);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid BrokerId)
        {
            var Broker = await _context.Brokers.FirstOrDefaultAsync(a => a.Id == BrokerId);
            if (Broker == null)
            {
                return false;
            }
            _context.Remove(Broker);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}