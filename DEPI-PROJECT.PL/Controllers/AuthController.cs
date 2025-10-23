using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
        [HttpPost]
        public IActionResult Register(AuthRegisterDto authRegisterDto)
        {
            return Ok(_authService.Register(authRegisterDto));
        }
    }
}
