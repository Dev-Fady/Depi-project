
using System.Security.Claims;
using DEPI_PROJECT.BLL.DTOs.Jwt;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(List<Claim> claims);
        // public bool InvokeToken();
        // public bool InvalidateToken();
    }
}