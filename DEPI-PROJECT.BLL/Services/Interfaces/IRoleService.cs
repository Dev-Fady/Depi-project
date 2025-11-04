using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.Role;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<ResponseDto<List<RoleResponseDto>>> GetAllAsync();
        public Task<ResponseDto<RoleResponseDto>> GetByName(string RoleName);
        public Task<ResponseDto<RoleResponseDto>> CreateAsync(RoleCreateDto roleCreateDto);
        public Task<ResponseDto<bool>> UpdateAsync(RoleUpdateDto roleUpdateDto);
        public Task<ResponseDto<bool>> DeleteAsync(Guid RoleId);

    }
}