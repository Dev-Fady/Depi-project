namespace DEPI_PROJECT.BLL.DTOs.UserRole
{
    public class UserRoleByRoleNameDto
    {
        public required Guid UserId { get; set; }
        public required string RoleName { get; set; }
    }
}