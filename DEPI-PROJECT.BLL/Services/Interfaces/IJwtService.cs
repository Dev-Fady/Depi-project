
using System.Security.Claims;
using Azure;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.DAL.Models;

namespace DEPI_PROJECT.BLL.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(List<Claim> claims);
        // public bool InvokeToken();
        // public bool InvalidateToken();
        public Task<ResponseDto<bool>> InvalidateToken(User user);
    }
}