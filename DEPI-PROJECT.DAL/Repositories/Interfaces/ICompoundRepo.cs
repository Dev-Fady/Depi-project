using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface ICompoundRepo
    {
        IQueryable<Compound> GetAllCompounds();
        Task<Compound?> GetCompoundByIdAsync(Guid id);

        Task AddCompoundAsync(Compound compound);
        Task UpdateCompoundAsync(Guid id, Compound compound);
        Task DeleteCompoundAsync(Guid id);
    }
}
