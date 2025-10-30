
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

        [HttpGet]
        // [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _userService.GetAllUsersAsync();
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);

        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetByIdAsync(Guid UserId)
        {
            var response = await _userService.GetUserByIdAsync(UserId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UserUpdateDto userUpdateDto)
        {
            var response = await _userService.UpdateUserAsync(userUpdateDto);
            if (response.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(response);
        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> DeleteAsync(Guid UserId)
        {
            var response = await _userService.DeleteUserAsync(UserId);
            if (response.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(response);
        }
        
    }
}
