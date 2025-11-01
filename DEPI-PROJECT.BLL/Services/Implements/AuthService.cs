
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.Role;
using DEPI_PROJECT.BLL.DTOs.UserRole;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Models.Enums;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;

        public AuthService(
            IJwtService jwtService,
            UserManager<User> userManager,
            IRoleService roleService,
            IUserRoleService userRoleService,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _roleService = roleService;
            _userRoleService = userRoleService;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        public async Task<ResponseDto<AuthResponseDto>> RegisterAsync(AuthRegisterDto authRegisterDto)
        {
            var roleResult = await _roleService.GetByName(authRegisterDto.RoleDiscriminator.ToString());
            if (!roleResult.IsSuccess)
            {
                return new ResponseDto<AuthResponseDto>
                {
                    Message = $"No Role exist with name {authRegisterDto.RoleDiscriminator}",
                    IsSuccess = false
                };
            }

            // Register the new user
            User user = _mapper.Map<AuthRegisterDto, User>(authRegisterDto);
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

            await _userManager.AddClaimsAsync(user, claims);

            // Assignning user to their role
            var result = await _userRoleService.AssignUserToRole(new UserRoleDto { UserId = user.UserId, RoleId = roleResult.Data.RoleId });

            if (!result.IsSuccess)
            {
                return new ResponseDto<AuthResponseDto>
                {
                    Message = result.Message,
                    IsSuccess = false
                };
            }

            string ToeknGenerated = _jwtService.GenerateToken(claims);

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