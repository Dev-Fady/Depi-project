using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.BLL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IResidentialPropertyService
    {
        Task<ResponseDto<PagedResultDto<ResidentialPropertyReadDto>>> GetAllResidentialPropertyAsync(Guid UserId, ResidentialPropertyQueryDto queryDto);
        Task<ResponseDto<ResidentialPropertyReadDto>> GetResidentialPropertyByIdAsync(Guid UserId, Guid id);
        Task<ResponseDto<ResidentialPropertyReadDto>> AddResidentialPropertyAsync(Guid UserId, ResidentialPropertyAddDto propertyDto);
        Task<ResponseDto<bool>> UpdateResidentialPropertyAsync(Guid UserId, Guid id, ResidentialPropertyUpdateDto propertyDto);
        Task<ResponseDto<bool>> DeleteResidentialPropertyAsync(Guid UserId, Guid id);
    }
}
