
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.DTOs.Jwt;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        public AuthService(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }
        public AuthResponseDto Register(AuthRegisterDto authRegisterDto)
        {
            string JwtToken = _jwtService.GenerateToken(new JwtCreateDto
            {
                UserName = authRegisterDto.UserName,
                Password = new PasswordHasher<string>().HashPassword(authRegisterDto.UserName, authRegisterDto.Password),
                Email = authRegisterDto.Email,
                userRole = Enum.GetName(typeof(UserRole), authRegisterDto.userRole)
            });

            return new AuthResponseDto
            {
                UserName = authRegisterDto.UserName,
                userRole = authRegisterDto.userRole,
                JwtToken = JwtToken
            };
        }
    }
}