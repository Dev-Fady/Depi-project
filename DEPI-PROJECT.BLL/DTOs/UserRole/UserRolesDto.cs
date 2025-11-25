using DEPI_PROJECT.BLL.DTOs.Role;

namespace DEPI_PROJECT.BLL.DTOs.UserRole
{
    public class UserRolesDto
    {
        public required Guid UserId { get; set; }
        public required List<RoleResponseDto> Roles { get; set; } = new List<RoleResponseDto>();

    }
}