using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.Authentication
{
    public class AuthLoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}