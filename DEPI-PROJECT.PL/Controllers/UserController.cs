
using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves all users with pagination and filtering options (Admin only)
        /// </summary>
        /// <param name="userQueryDto">Query parameters for filtering and pagination</param>
        /// <returns>Paginated list of users</returns>
        /// <response code="200">Returns the paginated list of users</response>
        /// <response code="400">If the request parameters are invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<UserResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllAsync([FromQuery] UserQueryDto userQueryDto)
        {
            var response = await _userService.GetAllUsersAsync(userQueryDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific user by their ID (Authenticated users only)
        /// </summary>
        /// <param name="UserId">The unique identifier of the user</param>
        /// <returns>User details if found</returns>
        /// <response code="200">Returns the user details</response>
        /// <response code="400">If the user is not found or request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpGet("{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(Guid UserId)
        {
            var response = await _userService.GetUserByIdAsync(UserId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing user's information (Authenticated users can update their own profile)
        /// </summary>
        /// <param name="userUpdateDto">Updated user details</param>
        /// <returns>Success status of the update operation</returns>
        /// <response code="200">Returns success if user is updated</response>
        /// <response code="400">If the update data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPut]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var response = await _userService.UpdateUserAsync(userUpdateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a user account (User can delete their own account or Admin can delete any account)
        /// </summary>
        /// <param name="UserId">The unique identifier of the user to delete</param>
        /// <returns>Success status of the delete operation</returns>
        /// <response code="200">Returns success if user is deleted</response>
        /// <response code="400">If the user is not found or request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpDelete("{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(Guid UserId)
        {
            var response = await _userService.DeleteUserAsync(UserId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        
    }
}
