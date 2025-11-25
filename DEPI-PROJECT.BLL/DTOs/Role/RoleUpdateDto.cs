namespace DEPI_PROJECT.BLL.DTOs.Role
{
    public class RoleUpdateDto
    {
        public required Guid RoleId { get; set; }
        public required string RoleName { get; set; }
    }
}