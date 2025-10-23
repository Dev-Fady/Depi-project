using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(AuthRegisterDto authRegisterDto)
        {
            var result = await _authService.RegisterAsync(authRegisterDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthLoginDto authLoginDto)
        {
            var result = await _authService.LoginAsync(authLoginDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("logout/{UserId}")]
        public async Task<IActionResult> Logout(Guid UserId)
        {
            // UserId = UserId.Trim();
            var result = await _authService.LogoutAsync(UserId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
            // return Ok();
        }

        // [HttpPost("add-role")]
        // public Task<IActionResult> AddRole()
        // {

        // }

        // [HttpPost("assign-user-to-role")]
        // public Task<IActionResult> AssignUserToRole()
        // {

        // }

        // [HttpDelete("remove-user-from-role")]
        // public Task<IActionResult> RemoveUserFromRole()
        // {

        // }

        // [HttpGet("roles")]
        // public Task<IActionResult> GetAllRoles()
        // {
            
        // }

        // [HttpGet("user-roles/{id}")]
        // public Task<IActionResult> GetUserRole(int id)
        // {

        // }
        
        
    }
}
