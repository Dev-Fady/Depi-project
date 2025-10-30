
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Principal;
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;
        public AuthService(
            IJwtService jwtService,
            UserManager<User> userManager
            )
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }
        public async Task<ResponseDto<AuthResponseDto>> RegisterAsync(AuthRegisterDto authRegisterDto)
        {
            // Register the new user
            User user = new User
            {
                UserName = authRegisterDto.UserName,
                Email = authRegisterDto.Email,
                PhoneNumber = authRegisterDto.Phone,
            };
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, authRegisterDto.Password);

            var identityResult = await _userManager.CreateAsync(user);

            if (!identityResult.Succeeded)
            {
                return new ResponseDto<AuthResponseDto>
                {
                    Message = "An error occured while registering the user. Please try again",
                    IsSuccess = false
                };
            }

            //Create Claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Version, "0"),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };


            string ToeknGenerated = _jwtService.GenerateToken(claims);

            await _userManager.AddClaimsAsync(user, claims);

            var authResponseDto = new AuthResponseDto
            {
                UserId = user.UserId,
                JwtToken = ToeknGenerated
            };

            return new ResponseDto<AuthResponseDto>
            {
                Data = authResponseDto,
                Message = "User Registered Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<AuthResponseDto>> LoginAsync(AuthLoginDto authLoginDto)
        {
            User? user = await _userManager.FindByNameAsync(authLoginDto.UserName);

            if (user == null)
            {
                return new ResponseDto<AuthResponseDto>
                {
                    Message = "No username match, please try another one",
                    IsSuccess = false
                };
            }

            bool VALID = await _userManager.CheckPasswordAsync(user, authLoginDto.Password);
            if(!VALID)
            {
                return new ResponseDto<AuthResponseDto>
                {
                    Message = "Password doesn't match the current username",
                    IsSuccess = false
                };
            }

            var claims = await _userManager.GetClaimsAsync(user);

            var token = _jwtService.GenerateToken(claims.ToList());

            var authResponseDto = new AuthResponseDto
            {
                UserId = user.UserId,
                JwtToken = token
            };

            return new ResponseDto<AuthResponseDto>
            {
                Data = authResponseDto,
                Message = "User logged successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> LogoutAsync(Guid userId)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ResponseDto<bool>
                {
                    Message = "No userId match found",
                    IsSuccess = false
                };
            }

            var result = await _jwtService.InvalidateToken(user);
            
            if (!result.IsSuccess)
            {
                return new ResponseDto<bool>
                {
                    Message = "An error occurred while logging out, please try again",
                    IsSuccess = false
                };
            }

            return new ResponseDto<bool>
            {
                Message = "User successfully logged out",
                IsSuccess = true
            };

        }
    }
}