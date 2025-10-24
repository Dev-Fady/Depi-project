using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Response;
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
        ResponseDto<PagedResult<CommercialPropertyReadDto>> GetAllProperties(int pageNumber, int pageSize);
        ResponseDto<CommercialPropertyReadDto> GetPropertyById(Guid id);

        ResponseDto<CommercialPropertyReadDto> AddProperty(CommercialPropertyAddDto propertyDto);

        ResponseDto<bool> UpdateCommercialProperty(Guid id, CommercialPropertyUpdateDto propertyDto);
        ResponseDto<bool> DeleteCommercialProperty(Guid id);
    }
}
