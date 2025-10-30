
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Azure;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public JwtService(IConfiguration configuration,
                          UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public string GenerateToken(List<Claim> claims)
        {

            string securityKey = _configuration.GetSection("JWT").GetSection("SecurityKey").Value;
            if (securityKey == null)
            {
                throw new Exception("No Section Named \"SecurityKey\" in section \"JWT\", Check your appsettings.json");
            }
            double ExpirationTimeInMinutes;

            if (!Double.TryParse(_configuration.GetSection("JWT").GetSection("ExpirationTimeInMinutes").Value, out ExpirationTimeInMinutes))
            {
                throw new Exception("No Section Named \"ExpirationTimeInMinutes\" in section \"JWT\", Check your appsettings.json");
            }

            var ByteSecurityKey = Encoding.ASCII.GetBytes(securityKey);
            SymmetricSecurityKey SSecurityKey = new SymmetricSecurityKey(ByteSecurityKey);
            SigningCredentials signingCredentials = new SigningCredentials(SSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(ExpirationTimeInMinutes),
                signingCredentials: signingCredentials
            );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        }

        // public bool InvokeToken()
        // {
        //     throw new NotImplementedException();
        // }
        
        public async Task<ResponseDto<bool>> InvalidateToken(User user){
            var claims = await _userManager.GetClaimsAsync(user);
            var OldClaim = claims.ToList().FirstOrDefault(c => c.Type == ClaimTypes.Version);
            int newTokenVersion = int.Parse(OldClaim.Value) + 1;


            var NewClaim = new Claim(OldClaim.Type, newTokenVersion.ToString());
            var identityResult = await _userManager.ReplaceClaimAsync(user, OldClaim, NewClaim);
            if (!identityResult.Succeeded)
            {
                return new ResponseDto<bool>
                {
                    message = "An error occured while invalidating the token",
                    IsSuccess = false
                };
            }
            return new ResponseDto<bool>
            {
                message = "Token invalidated successfully",
                IsSuccess = true
            };
        }
    }
}