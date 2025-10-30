using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.Role;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.BLL.DTOs.UserRole;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IUserRoleService
    {
        public Task<ResponseDto<List<UserResponseDto>>> GetUsersFromRole(Guid RoleId);
        public Task<ResponseDto<UserRolesDto>> GetRolesFromUser(Guid UserId);
        public Task<ResponseDto<bool>> AssignUserToRole(UserRoleDto userRoleDto);
        public Task<ResponseDto<bool>> RemoveUserFromRole(UserRoleDto userRoleDto);


    }
}