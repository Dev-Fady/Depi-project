using DEPI_PROJECT.BLL.DTOs.Pagination;
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
        PagedResult<ResidentialProperty> GetAllResidentialProperty(int pageNumber, int pageSize);
        ResidentialProperty GetResidentialPropertyById(Guid id);

        void AddResidentialProperty(ResidentialProperty property);
        void UpdateResidentialProperty(Guid id,ResidentialProperty property);
        void DeleteResidentialProperty(Guid id);

        void AddAmenity(Amenity amenity);
        void UpdateAmenity(Amenity amenity);

    }
}
