using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.DAL.Repository.ResidentialProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Manager.CommercialProperty
{
    public interface ICommercialPropertyManager
    {
        PagedResult<CommercialPropertyReadDto> GetAllProperties(int pageNumber, int pageSize);
        CommercialPropertyReadDto GetPropertyById(Guid id);

        CommercialPropertyReadDto AddProperty(CommercialPropertyAddDto propertyDto);

        bool UpdateCommercialProperty(Guid id, CommercialPropertyUpdateDto propertyDto);
        bool DeleteCommercialProperty(Guid id);
    }
}
