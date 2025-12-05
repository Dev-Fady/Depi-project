using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface IResidentialPropertyRepo
    {
        IQueryable<ResidentialProperty> GetAllResidentialProperty(Guid CurrentUserid);
        Task<ResidentialProperty?> GetResidentialPropertyByIdAsync(Guid CurrentUserid, Guid id);

        Task AddResidentialPropertyAsync(ResidentialProperty property);
        Task UpdateResidentialPropertyAsync(Guid id,ResidentialProperty property);
        Task DeleteResidentialPropertyAsync(Guid id);

    }
}
