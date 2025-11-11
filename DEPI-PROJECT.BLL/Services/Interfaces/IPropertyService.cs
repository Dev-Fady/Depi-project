using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<ResponseDto<PagedResultDto<AllPropertyReadDto>>> GetAll(PropertyQueryDto propertyQueryDto);
        public Task<bool> CheckPropertyExist(Guid PropertyId);
    }
}