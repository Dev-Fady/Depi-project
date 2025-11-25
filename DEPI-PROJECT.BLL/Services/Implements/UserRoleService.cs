using System.Security.Claims;
using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.Role;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.BLL.DTOs.UserRole;
using DEPI_PROJECT.BLL.Exceptions;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UserRoleService(UserManager<User> userManager,
                               RoleManager<Role> roleManager,
                               IJwtService jwtService,
                               IMapper mapper,
                               AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
            _mapper = mapper;
            _context = context;
        }
        public async Task<ResponseDto<List<UserResponseDto>>> GetUsersFromRole(Guid RoleId)
        {
            Role? role = await _roleManager.FindByIdAsync(RoleId.ToString());

            if (role == null)
            {
                throw new NotFoundException($"No role found with Id {RoleId}");
            }


            var users = await _userManager.GetUsersInRoleAsync(role.Name!);

            var userResponseDtos = _mapper.Map<IEnumerable<User>, IEnumerable<UserResponseDto>>(users);

            return new ResponseDto<List<UserResponseDto>>
            {
                Data = userResponseDtos.ToList(),
                Message = "Users Returned successfully",
                IsSuccess = true
            };
        }
        
        public async Task<ResponseDto<UserRolesDto>> GetRolesFromUser(Guid UserId)
        {
        
            var User = await _userManager.FindByIdAsync(UserId.ToString());
            if (User == null)
            {
                throw new NotFoundException($"No User associated with the given ID {UserId}");
            }
            var Roles = await _GetRolesFromUser(User);

            UserRolesDto userRolesDto = new UserRolesDto
            {
                UserId = UserId,
                Roles = Roles
            };

            return new ResponseDto<UserRolesDto>
            {
                Data = userRolesDto,
                Message = "Roles retrived successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> AssignUserToRole(UserRoleDto userRoleDto)
        {
            (User user, Role role) = await GetUserAndRole(userRoleDto);

            var identityResult = await _userManager.AddToRoleAsync(user, role.Name!);
            if (!identityResult.Succeeded)
            {
                throw new Exception(identityResult.Errors.ElementAt(0).Description
                            ?? "An error occurred while assignning user role");
            }

            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.Role, role.NormalizedName!)
            };

            identityResult = await _userManager.AddClaimsAsync(user, claims);

            if (!identityResult.Succeeded)
            {
                throw new Exception(identityResult.Errors.ElementAt(0).Description
                            ?? $"An error occurred while adding user to role {role.Name}");
            }

            var result = await _jwtService.InvalidateToken(user);

            return new ResponseDto<bool>
            {
                Message = $"User added to role {role.Name}",
                IsSuccess = true
            };
        }
        
        public async Task<ResponseDto<bool>> RemoveUserFromRoleByRoleName(UserRoleByRoleNameDto userRoleDto){
            var Role = await _roleManager.FindByNameAsync(userRoleDto.RoleName);
            if (Role == null)
            {
                throw new NotFoundException($"No role found with name {userRoleDto.RoleName}");
            }
            var response = await RemoveUserFromRole(new UserRoleDto { UserId = userRoleDto.UserId, RoleId = Role.Id });
            return response;
        }

        public async Task<ResponseDto<bool>> RemoveUserFromRole(UserRoleDto userRoleDto)
        {
            (User user, Role role) = await GetUserAndRole(userRoleDto);

            var identityResult = await _userManager.RemoveFromRoleAsync(user, role.Name!);
            if (!identityResult.Succeeded)
            {
                throw new Exception(identityResult.Errors.ElementAt(0).Description
                        ?? $"An error occurred while removing user from role {role.Name}");
            }

            var claims = await _userManager.GetClaimsAsync(user);

            var roleClaim = claims.FirstOrDefault(a => a.Type == ClaimTypes.Role);
            if (roleClaim == null)
            {
                // User has no role claim to remove, but this might be OK
                return new ResponseDto<bool>
                {
                    Message = $"User removed from role {role.Name} successfully (no role claim found)",
                    IsSuccess = true
                };
            }

            List<Claim> claimsToDelete = [roleClaim];
            
            identityResult = await _userManager.RemoveClaimsAsync(user, claimsToDelete);
            if (!identityResult.Succeeded)
            {
                throw new Exception(identityResult.Errors.ElementAt(0).Description 
                        ?? $"An error occurred while removing claim role {role.NormalizedName}");
            }

            var result = await _jwtService.InvalidateToken(user);
            
            return new ResponseDto<bool>
            {
                Message = $"User removed from role {role.Name} successfully",
                IsSuccess = true
            };
        }

        private async Task<Tuple<User, Role>> GetUserAndRole(UserRoleDto userRoleDto)
        {
            var role = await _roleManager.FindByIdAsync(userRoleDto.RoleId.ToString());
            if (role == null)
            {
                throw new NotFoundException($"No role found with Id {userRoleDto.RoleId}");
            }

            var user = await _userManager.FindByIdAsync(userRoleDto.UserId.ToString());
            if (user == null)
            {
                throw new NotFoundException($"No user found with Id {userRoleDto.UserId}");
            }

            return new(user, role);
        }

        private async Task<List<RoleResponseDto>> _GetRolesFromUser(User user)
        {
            var Roles = await _context.UserRoles
                    .Where(ur => ur.UserId == user.Id)
                    .Join(_context.Roles,
                          ur => ur.RoleId,
                          r => r.Id,
                          (ur, r) => new RoleResponseDto{
                            RoleId = ur.RoleId,
                            RoleName = r.Name!
                          }
                    )
                    .ToListAsync();
            return Roles;
        }
    }
}