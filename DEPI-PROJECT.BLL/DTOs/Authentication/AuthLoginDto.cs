using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.Authentication
{
    public class AuthLoginDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}