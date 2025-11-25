using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.Authentication
{
    public class AuthResponseDto
    {
        public required Guid UserId { get; set; }
        public required string JwtToken { get; set; }   
    }
}