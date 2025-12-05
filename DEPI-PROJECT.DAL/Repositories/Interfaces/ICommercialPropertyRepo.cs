using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DEPI_PROJECT.DAL.Repositories.Interfaces
{
    public interface ICommercialPropertyRepo
    {
        IQueryable<CommercialProperty> GetAllProperties(Guid CurrentUserid);
        Task<CommercialProperty?> GetPropertyByIdAsync(Guid CurrentUserid, Guid id);
        Task AddCommercialPropertyAsync(CommercialProperty property);
        Task UpdateCommercialPropertyAsync(Guid id, CommercialProperty property);
        Task DeleteCommercialPropertyAsync(Guid id);

    }
}
