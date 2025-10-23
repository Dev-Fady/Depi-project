
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DEPI_PROJECT.BLL.DTOs.Jwt;
using DEPI_PROJECT.BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(List<Claim> claims)
        {
            
            string securityKey = _configuration.GetSection("JWT").GetSection("SecurityKey").Value;
            if(securityKey == null)
            {
                throw new Exception("No Section Named \"SecurityKey\" in section \"JWT\", Check your appsettings.json");
            }
            double ExpirationTimeInMinutes;

            if(!Double.TryParse(_configuration.GetSection("JWT").GetSection("ExpirationTimeInMinutes").Value, out ExpirationTimeInMinutes))
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
    }
}