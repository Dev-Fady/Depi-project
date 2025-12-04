
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.Role;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Retrieves all available roles in the system (Admin only)
        /// </summary>
        /// <returns>List of all roles</returns>
        /// <response code="200">Returns the list of all roles</response>
        /// <response code="400">If the request fails</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<List<RoleResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _roleService.GetAllAsync();
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific role by its name (Admin only)
        /// </summary>
        /// <param name="RoleName">The name of the role to retrieve</param>
        /// <returns>Role details if found</returns>
        /// <response code="200">Returns the role details</response>
        /// <response code="400">If the role is not found</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpGet("{RoleName}")]
        [ProducesResponseType(typeof(ResponseDto<RoleResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByNameAsync(string RoleName){
            var response = await _roleService.GetByName(RoleName);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Creates a new role in the system (Admin only)
        /// </summary>
        /// <param name="roleCreateDto">Role details for creation</param>
        /// <returns>Created role details</returns>
        /// <response code="200">Returns the newly created role</response>
        /// <response code="400">If the role data is invalid or role already exists</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<RoleResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(RoleCreateDto roleCreateDto)
        {
            var response = await _roleService.CreateAsync(roleCreateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing role's information (Admin only)
        /// </summary>
        /// <param name="roleUpdateDto">Updated role details</param>
        /// <returns>Success status of the update operation</returns>
        /// <response code="200">Returns success if role is updated</response>
        /// <response code="400">If the update data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpPut]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(RoleUpdateDto roleUpdateDto)
        {
            var response = await _roleService.UpdateAsync(roleUpdateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a role from the system (Admin only)
        /// </summary>
        /// <param name="RoleId">The unique identifier of the role to delete</param>
        /// <returns>Success status of the delete operation</returns>
        /// <response code="200">Returns success if role is deleted</response>
        /// <response code="400">If the role is not found or cannot be deleted</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpDelete("{RoleId}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(Guid RoleId)
        {
            var response = await _roleService.DeleteAsync(RoleId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
