
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
using DEPI_PROJECT.BLL.Exceptions;
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
                throw new NotFoundException($"No Role exist with name {authRegisterDto.RoleDiscriminator}");
            }

            // Register the new user
            User user = _mapper.Map<AuthRegisterDto, User>(authRegisterDto);
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, authRegisterDto.Password);

            var identityResult = await _userManager.CreateAsync(user);

            if (!identityResult.Succeeded)
            {
                throw new Exception(identityResult.Errors.ElementAt(0).Description
                        ?? "An error occured while registering the user. Please try again");
            }

            //Create Claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Version, "0"),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            identityResult = await _userManager.AddClaimsAsync(user, claims);
            if (!identityResult.Succeeded)
            {
                throw new Exception(identityResult.Errors.ElementAt(0).Description
                        ?? "An error occured while adding user claims. Please try again");
            }
            
            // Assignning user to their role
            await _userRoleService.AssignUserToRole(new UserRoleDto { UserId = user.UserId, RoleId = roleResult.Data.RoleId });

            var allClaims = await _userManager.GetClaimsAsync(user);
            string TokenGenerated = _jwtService.GenerateToken(allClaims.ToList());

            var authResponseDto = new AuthResponseDto
            {
                UserId = user.UserId,
                JwtToken = TokenGenerated
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
                throw new NotFoundException($"No user found with the username {authLoginDto.UserName}");
            }

            bool VALID = await _userManager.CheckPasswordAsync(user, authLoginDto.Password);
            if(!VALID)
            {
                throw new BadRequestException("Password doesn't match the current username");
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
                throw new NotFoundException($"No user found with user Id {userId}");
            }

            await _jwtService.InvalidateToken(user);

            return new ResponseDto<bool>
            {
                Message = "User successfully logged out",
                IsSuccess = true
            };

        }
    }
}