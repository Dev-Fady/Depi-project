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
        IQueryable<ResidentialProperty> GetAllResidentialProperty();
        Task<ResidentialProperty?> GetResidentialPropertyByIdAsync(Guid id);

        Task AddResidentialPropertyAsync(ResidentialProperty property);
        Task UpdateResidentialPropertyAsync(Guid id,ResidentialProperty property);
        Task DeleteResidentialPropertyAsync(Guid id);
        Task AddAmenityAsync(Amenity amenity);
        Task UpdateAmenityAsync(Amenity amenity);

    }
}
