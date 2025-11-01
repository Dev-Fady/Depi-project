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
        Task<ResponseDto<PagedResultDto<CompoundReadDto>>> GetAllCompoundsAsync(CompoundQueryDto compoundQueryDto);
        Task<ResponseDto<CompoundReadDto>> GetCompoundByIdAsync(Guid id);
        Task<ResponseDto<CompoundReadDto>> AddCompoundAsync(CompoundAddDto compoundDto);
        Task<ResponseDto<bool>> UpdateCompoundAsync(Guid id, CompoundUpdateDto compoundDto);
        Task<ResponseDto<bool>> DeleteCompoundAsync(Guid id);
    }
}
