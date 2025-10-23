using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.Authentication
{
    public class AuthResponseDto
    {
        public string UserName { get; set; }
        public UserRole userRole { get; set; }
        public string JwtToken { get; set; }
        
    }
}