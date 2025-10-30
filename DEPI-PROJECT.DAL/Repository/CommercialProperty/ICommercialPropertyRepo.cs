using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Repository.ResidentialProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.DAL.Repository.CommercialProperty
{
    public interface ICommercialPropertyRepo
    {
        PagedResult<Models.CommercialProperty> GetAllProperties(int pageNumber, int pageSize);

        Models.CommercialProperty GetPropertyById(Guid id);

        void AddCommercialProperty(Models.CommercialProperty property);
        void UpdateCommercialProperty(Guid id, Models.CommercialProperty property);
        void DeleteCommercialProperty(Guid id);

        void AddAmenity(Amenity amenity);
        void UpdateAmenity(Amenity amenity);

    }
}
