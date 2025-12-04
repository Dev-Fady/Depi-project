using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.DTOs.Response;
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
        /// <summary>
        /// Registers a new user account
        /// </summary>
        /// <param name="authRegisterDto">User registration details including username, email, and password</param>
        /// <returns>Authentication response with JWT token if successful</returns>
        /// <response code="200">Returns authentication token and user details</response>
        /// <response code="400">If registration data is invalid or user already exists</response>
        [HttpPost("Register")]
        [ProducesResponseType(typeof(ResponseDto<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(AuthRegisterDto authRegisterDto)
        {
            var result = await _authService.RegisterAsync(authRegisterDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            
            return Ok(result);
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token
        /// </summary>
        /// <param name="authLoginDto">User login credentials (username/email and password)</param>
        /// <returns>Authentication response with JWT token if credentials are valid</returns>
        /// <response code="200">Returns authentication token and user details</response>
        /// <response code="400">If login credentials are invalid</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseDto<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(AuthLoginDto authLoginDto)
        {
            var result = await _authService.LoginAsync(authLoginDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Logs out a user by invalidating their JWT token
        /// </summary>
        /// <param name="UserId">The unique identifier of the user to logout</param>
        /// <returns>Success status of the logout operation</returns>
        /// <response code="200">Returns success if user is logged out</response>
        /// <response code="400">If the user ID is invalid or logout fails</response>
        [HttpPost("logout/{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout(Guid UserId)
        {
            // UserId = UserId.Trim();
            var result = await _authService.LogoutAsync(UserId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
