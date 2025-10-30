using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface ICommercialPropertyService
    {
        ResponseDto<PagedResult<CommercialPropertyReadDto>> GetAllProperties(int pageNumber, int pageSize);
        ResponseDto<CommercialPropertyReadDto> GetPropertyById(Guid id);

        ResponseDto<CommercialPropertyReadDto> AddProperty(CommercialPropertyAddDto propertyDto);

        ResponseDto<bool> UpdateCommercialProperty(Guid id, CommercialPropertyUpdateDto propertyDto);
        ResponseDto<bool> DeleteCommercialProperty(Guid id);
    }
}
