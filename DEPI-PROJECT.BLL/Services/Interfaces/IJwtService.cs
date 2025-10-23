
using DEPI_PROJECT.BLL.DTOs.Jwt;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(JwtCreateDto jwtCreateDto);
        public bool InvokeToken();
    }
}