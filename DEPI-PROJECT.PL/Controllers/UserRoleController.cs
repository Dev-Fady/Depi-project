using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.BLL.DTOs.UserRole;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpGet("users-role/{RoleId}")]
        [ProducesResponseType(typeof(ResponseDto<List<UserResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetUsersRole(Guid RoleId)
        {
            var response = await _userRoleService.GetUsersFromRole(RoleId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("user-roles/{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<UserRolesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetUserRolesAsync(Guid UserId)
        {
            var response = await _userRoleService.GetRolesFromUser(UserId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("assign-user-to-role")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AssignToRoleAsync(UserRoleDto userRoleDto)
        {
            var response = await _userRoleService.AssignUserToRole(userRoleDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("remove-user-from-role")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RemoveFromRoleAsync(UserRoleDto userRoleDto)
        {
            var response = await _userRoleService.RemoveUserFromRole(userRoleDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


    }
}