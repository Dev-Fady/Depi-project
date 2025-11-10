using AutoMapper;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.Role;
using DEPI_PROJECT.BLL.Exceptions;
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
        private readonly IMapper _mapper;

        public RoleService(
            RoleManager<Role> roleManager,
            IMapper mapper
            )
        {
            // _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
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
                    Message = "No roles found",
                    IsSuccess = true
                };
            }

            return new ResponseDto<List<RoleResponseDto>>
            {
                Data = roles,
                Message = "Roles reterived successfully",
                IsSuccess = true
            };
        }
        
        public async Task<ResponseDto<RoleResponseDto>> GetByName(string RoleName)
        {
            Role role = await _roleManager.FindByNameAsync(RoleName);
            if (role == null)
            {
                throw new NotFoundException($"No role found with name {RoleName}");
            }

            RoleResponseDto roleResponseDto = _mapper.Map<Role, RoleResponseDto>(role);

            return new ResponseDto<RoleResponseDto>
            {
                Data = roleResponseDto,
                Message = "Role retrived successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<RoleResponseDto>> CreateAsync(RoleCreateDto roleCreateDto)
        {
            var role = _mapper.Map<RoleCreateDto, Role>(roleCreateDto);

            var identityResult = await _roleManager.CreateAsync(role);
            if (!identityResult.Succeeded)
            {
                throw new BadRequestException(identityResult.Errors.ElementAt(0).Description
                        ?? "An error occurred while creating the role, please try again");
            }

            var roleResponseDto = _mapper.Map<Role, RoleResponseDto>(role);

            return new ResponseDto<RoleResponseDto>
            {
                Data = roleResponseDto,
                Message = "Role Created successfully",
                IsSuccess = true
            };

        }

        public async Task<ResponseDto<bool>> UpdateAsync(RoleUpdateDto roleUpdateDto)
        {
            var role = await _roleManager.FindByIdAsync(roleUpdateDto.RoleId.ToString());

            if (role == null)
            {
                throw new NotFoundException($"No role found with Id {roleUpdateDto.RoleId}");
            }

            _mapper.Map<RoleUpdateDto, Role>(roleUpdateDto, role);

            var identityResult = await _roleManager.UpdateAsync(role);
            
            if (!identityResult.Succeeded)
            {
                throw new BadRequestException(identityResult.Errors.ElementAt(0).Description 
                        ?? "An error occurred while updating the role, please try again");
            }

            return new ResponseDto<bool>
            {
                Message = "Role updated successfully",
                IsSuccess = true
            }; 
        }

        public async Task<ResponseDto<bool>> DeleteAsync(Guid RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId.ToString());

            if (role == null)
            {
                throw new NotFoundException($"No role found with Id {RoleId}");

            }
            
            var identityResult = await _roleManager.DeleteAsync(role);
            if (!identityResult.Succeeded)
            {
                throw new BadRequestException(identityResult.Errors.ElementAt(0).Description
                        ?? "An error occurred while deleting the role, please try again");
            }

            return new ResponseDto<bool>
            {
                Message = "Role deleted successfully",
                IsSuccess = true
            }; 
        }


    }
}