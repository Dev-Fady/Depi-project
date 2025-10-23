using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.Authentication
{
    public class AuthResponseDto
    {
        public Guid UserId { get; set; }
        public string JwtToken { get; set; }   
    }
}