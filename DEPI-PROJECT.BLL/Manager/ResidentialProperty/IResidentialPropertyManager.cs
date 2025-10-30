using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.DAL.Repository.ResidentialProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Manager.ResidentialProperty
{
    public interface IResidentialPropertyManager
    {
        ResponseDto<PagedResult<ResidentialPropertyReadDto>> GetAllResidentialProperty(int pageNumber, int pageSize);
        ResponseDto<ResidentialPropertyReadDto> GetResidentialPropertyById(Guid id);
        ResponseDto<ResidentialPropertyReadDto> AddResidentialProperty(ResidentialPropertyAddDto propertyDto);
        ResponseDto<bool> UpdateResidentialProperty(Guid id, ResidentialPropertyUpdateDto propertyDto);
        ResponseDto<bool> DeleteResidentialProperty(Guid id);
    }
}
