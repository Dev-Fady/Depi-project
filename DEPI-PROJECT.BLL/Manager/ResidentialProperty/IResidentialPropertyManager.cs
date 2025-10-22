using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
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
        PagedResult<ResidentialPropertyReadDto> GetAllResidentialProperty(int pageNumber, int pageSize);
        ResidentialPropertyReadDto GetResidentialPropertyById(Guid id);
        ResidentialPropertyReadDto AddResidentialProperty(ResidentialPropertyAddDto propertyDto);
        bool UpdateResidentialProperty(Guid id, ResidentialPropertyUpdateDto propertyDto);
        bool DeleteResidentialProperty(Guid id);
    }
}
