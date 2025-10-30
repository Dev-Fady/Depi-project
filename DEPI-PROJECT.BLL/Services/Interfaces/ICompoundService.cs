using DEPI_PROJECT.BLL.DTOs.Compound;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface ICompoundService
    {
        ResponseDto<PagedResult<CompoundReadDto>> GetAllCompounds(int pageNumber, int pageSize);
        ResponseDto<CompoundReadDto> GetCompoundById(Guid id);
        ResponseDto<CompoundReadDto> AddCompound(CompoundAddDto compoundDto);
        ResponseDto<bool> UpdateCompound(Guid id, CompoundUpdateDto compoundDto);
        ResponseDto<bool> DeleteCompound(Guid id);
    }
}
