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
        Task<ResponseDto<PagedResultDto<CommercialPropertyReadDto>>> GetAllPropertiesAsync(Guid UserId, CommercialPropertyQueryDto queryDto);
        Task<ResponseDto<CommercialPropertyReadDto>> GetPropertyByIdAsync(Guid UserId, Guid id);
        Task<ResponseDto<CommercialPropertyReadDto>> AddPropertyAsync(Guid UserId, Guid AgentId, CommercialPropertyAddDto propertyDto);
        Task<ResponseDto<bool>> UpdateCommercialPropertyAsync(Guid UserId, Guid id, CommercialPropertyUpdateDto propertyDto);
        Task<ResponseDto<bool>> DeleteCommercialPropertyAsync(Guid UserId, Guid id);
    }
}
