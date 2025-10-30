using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.Role;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DEPI_PROJECT.BLL.Services.Implements
{
    public class RoleService : IRoleService
    {
        // private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            // _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResponseDto<List<RoleResponseDto>>> GetAllAsync()
        {
            var roles = await _roleManager.Roles
                            .Select(r => new RoleResponseDto
                                {
                                    RoleId = r.Id,
                                    RoleName = r.Name
                                }
                            )
                            .ToListAsync();

            if (roles.Count == 0)
            {
                return new ResponseDto<List<RoleResponseDto>>
                {
                    message = "No roles found",
                    IsSuccess = true
                };
            }

            return new ResponseDto<List<RoleResponseDto>>
            {
                Data = roles,
                message = "Roles reterived successfully",
                IsSuccess = true
            };
        }
        public async Task<ResponseDto<RoleResponseDto>> CreateAsync(RoleCreateDto roleCreateDto)
        {
            var role = new Role
            {
                Name = roleCreateDto.RoleName,
                NormalizedName = roleCreateDto.RoleName.ToLower()
            };

            var identityResult = await _roleManager.CreateAsync(role);
            if (!identityResult.Succeeded)
            {
                return new ResponseDto<RoleResponseDto>
                {
                    message = "An error occurred while creating the role, please try again",
                    IsSuccess = false
                };
            }

            var roleResponseDto = new RoleResponseDto
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            return new ResponseDto<RoleResponseDto>
            {
                Data = roleResponseDto,
                message = "Role Created successfully",
                IsSuccess = true
            };

        }

        public async Task<ResponseDto<bool>> UpdateAsync(RoleUpdateDto roleUpdateDto)
        {
            var role = await _roleManager.FindByIdAsync(roleUpdateDto.RoleId.ToString());

            if (role == null)
            {
                return new ResponseDto<bool>
                {
                    message = "No response with the specified ID",
                    IsSuccess = false
                };
            }

            role.Name = roleUpdateDto.RoleName;
            role.NormalizedName = roleUpdateDto.RoleName.ToLower();

            var identityResult = await _roleManager.UpdateAsync(role);
            
            if (!identityResult.Succeeded)
            {
                return new ResponseDto<bool>
                {
                    message = "An error occurred while updating the role, please try again",
                    IsSuccess = false
                };
            }

            return new ResponseDto<bool>
            {
                message = "Role updated successfully",
                IsSuccess = true
            }; 
        }

        public async Task<ResponseDto<bool>> DeleteAsync(Guid RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId.ToString());

            if (role == null)
            {
                return new ResponseDto<bool>
                {
                    message = $"No role found with Id: {RoleId}",
                    IsSuccess = false
                };
            }
            
            var identityResult = await _roleManager.DeleteAsync(role);
            if (!identityResult.Succeeded)
            {
                return new ResponseDto<bool>
                {
                    message = "An error occurred while deleting the role, please try again",
                    IsSuccess = false
                };
            }

            return new ResponseDto<bool>
            {
                message = "Role deleted successfully",
                IsSuccess = true
            }; 
        }


    }
}