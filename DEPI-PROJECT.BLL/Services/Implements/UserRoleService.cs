using System.Security.Claims;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.Role;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.BLL.DTOs.UserRole;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class UserRoleService : IUserRoleService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IJwtService _jwtService;

        public UserRoleService(UserManager<User> userManager,
                               RoleManager<Role> roleManager,
                               IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }
        public async Task<ResponseDto<List<UserResponseDto>>> GetUsersFromRole(Guid RoleId)
        {
            Role role = await _roleManager.FindByIdAsync(RoleId.ToString());

            if (role == null)
            {
                return new ResponseDto<List<UserResponseDto>>
                {
                    Data = [],
                    message = $"No role found by id {RoleId}",
                    IsSuccess = false
                };
            }


            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            
            if(users.Count == 0)
            {
                return new ResponseDto<List<UserResponseDto>>
                {
                    Data = [],
                    message = "No user associated with the current role",
                    IsSuccess = true
                };
            }

            List<UserResponseDto> userResponseDtos = new List<UserResponseDto>();

            foreach (var user in users)
            {
                var userResponseDto = new UserResponseDto
                {
                    UserId = user.UserId,
                    Username = user.UserName,
                    Email = user.Email,
                    phone = user.PhoneNumber,
                    DateJoined = user.DateJoined,
                    RoleName = role.Name
                };
                userResponseDtos.Add(userResponseDto);
            }

            return new ResponseDto<List<UserResponseDto>>
            {
                Data = userResponseDtos,
                message = "Users Returned successfully",
                IsSuccess = true
            };
        }
        
        public async Task<ResponseDto<UserRolesDto>> GetRolesFromUser(Guid UserId)
        {
        
            var User = await _userManager.FindByIdAsync(UserId.ToString());
            if (User == null)
            {
                return new ResponseDto<UserRolesDto>
                {
                    message = $"No User associated with the given ID {UserId}",
                    IsSuccess = false
                };
            }
            var Roles = await _userManager.GetRolesAsync(User);

            if(Roles.Count == 0)
            {
                return new ResponseDto<UserRolesDto>
                {
                    Data = null,
                    message = "No Roles associated with the current user",
                    IsSuccess = true
                };
            }

            UserRolesDto userRolesDto = new UserRolesDto
            {
                UserId = UserId,
                Roles = (List<string>)Roles
            };

            return new ResponseDto<UserRolesDto>
            {
                Data = userRolesDto,
                message = "Roles retrived successfully",
                IsSuccess = true
            };
        }
        
        public async Task<ResponseDto<bool>> AssignUserToRole(UserRoleDto userRoleDto)
        {
            (User? user, Role? role, ResponseDto<bool> responseDto) = await GetUserAndRole(userRoleDto);

            if (user == null || role == null)
            {
                return responseDto;
            }
            


            var identityResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!identityResult.Succeeded)
            {
                return new ResponseDto<bool>
                {
                    message = "An error occurred while assignning user role",
                    IsSuccess = false
                };
            }
            // Claim claim = new Claim(ClaimTypes.Role, role.NormalizedName);
            // identityResult = await _userManager.AddClaimAsync(user, claim);

            // if (!identityResult.Succeeded)
            // {
            //     return new ResponseDto<bool>
            //     {
            //         message = $"An error occurred while adding role claim {role.NormalizedName}",
            //         IsSuccess = false
            //     };
            // }

            // var result = await _jwtService.InvalidateToken(user);

            // if (!result.IsSuccess)
            // {
            //     return result;
            // }


            return new ResponseDto<bool>
            {
                message = $"User added to role {role.Name}",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> RemoveUserFromRole(UserRoleDto userRoleDto)
        {
            (User? user, Role? role, ResponseDto<bool> responseDto) = await GetUserAndRole(userRoleDto);

            if (user == null || role == null)
            {
                return responseDto;
            }

            var identityResult = await _userManager.RemoveFromRoleAsync(user, role.Name);
            
            if (!identityResult.Succeeded)
            {
                return new ResponseDto<bool>
                {
                    message = $"An error occurred while removing user from role {role.Name}",
                    IsSuccess = false
                };
            }
            // Claim claim = new Claim(ClaimTypes.Role, role.NormalizedName);

            // identityResult = await _userManager.RemoveClaimAsync(user, claim);

            // if (!identityResult.Succeeded)
            // {
            //     return new ResponseDto<bool>
            //     {
            //         message = $"An error occurred while removing claim role {role.NormalizedName}",
            //         IsSuccess = false
            //     };
            // }

            // var result = await _jwtService.InvalidateToken(user);

            // if (!result.IsSuccess)
            // {
            //     return result;
            // }
            return new ResponseDto<bool>
            {
                message = $"User removed from role {role.Name} successfully",
                IsSuccess = true
            };
        }

        private async Task<Tuple<User?, Role?, ResponseDto<bool>>> GetUserAndRole(UserRoleDto userRoleDto)
        {
            var role = await _roleManager.FindByIdAsync(userRoleDto.RoleId.ToString());
            if (role == null)
            {
                return new(
                    null,
                    null,
                    new ResponseDto<bool>
                    {
                        message = $"No role found with Id {userRoleDto.RoleId}",
                        IsSuccess = false
                    }
                ); 
            }

            var user = await _userManager.FindByIdAsync(userRoleDto.UserId.ToString());
            if (user == null)
            {
                return new(
                    null,
                    null,
                    new ResponseDto<bool>
                    {
                        message = $"No role found with Id {userRoleDto.RoleId}",
                        IsSuccess = false
                    }
                );
            }

            return new (
                    user,
                    role,
                    new ResponseDto<bool>
                    {
                        message = "User and Role retrived",
                        IsSuccess = true
                    }
                );

        }
    }
}