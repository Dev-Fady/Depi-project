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

        /// <summary>
        /// Retrieves all users assigned to a specific role (Admin only)
        /// </summary>
        /// <param name="RoleId">The unique identifier of the role</param>
        /// <returns>List of users with the specified role</returns>
        /// <response code="200">Returns the list of users in the role</response>
        /// <response code="400">If the role is not found</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
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

        /// <summary>
        /// Retrieves all roles assigned to a specific user (Authenticated users only)
        /// </summary>
        /// <param name="UserId">The unique identifier of the user</param>
        /// <returns>List of roles assigned to the user</returns>
        /// <response code="200">Returns the user's roles</response>
        /// <response code="400">If the user is not found</response>
        /// <response code="401">If the user is not authenticated</response>
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

        /// <summary>
        /// Assigns a user to a specific role (Admin only)
        /// </summary>
        /// <param name="userRoleDto">User and role assignment details</param>
        /// <returns>Success status of the role assignment</returns>
        /// <response code="200">Returns success if user is assigned to role</response>
        /// <response code="400">If the assignment data is invalid or assignment fails</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
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

        /// <summary>
        /// Removes a user from a specific role (Admin only)
        /// </summary>
        /// <param name="userRoleDto">User and role removal details</param>
        /// <returns>Success status of the role removal</returns>
        /// <response code="200">Returns success if user is removed from role</response>
        /// <response code="400">If the removal data is invalid or removal fails</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
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