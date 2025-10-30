using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface ICompoundRepo
    {
        PagedResult<Compound> GetAllCompounds(int pageNumber, int pageSize);
        Compound GetCompoundById(Guid id);

        void AddCompound(Compound compound);
        void UpdateCompound(Guid id, Compound compound);
        void DeleteCompound(Guid id);
    }
}
