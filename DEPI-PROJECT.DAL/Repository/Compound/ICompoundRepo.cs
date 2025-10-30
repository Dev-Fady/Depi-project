using DEPI_PROJECT.DAL.Repository.ResidentialProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Repository.Compound
{
    public interface ICompoundRepo
    {
        PagedResult<Models.Compound> GetAllCompounds(int pageNumber, int pageSize);
        Models.Compound GetCompoundById(Guid id);

        void AddCompound(Models.Compound compound);
        void UpdateCompound(Guid id, Models.Compound compound);
        void DeleteCompound(Guid id);
    }
}
