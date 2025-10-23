using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // private readonly IdentityDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(
            // IdentityDbContext identityDbContext,
            UserManager<User> userManager)
        {
            // _context = identityDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userManager.Users.ToListAsync();

            return Ok(result);
        }
        
    }
}
