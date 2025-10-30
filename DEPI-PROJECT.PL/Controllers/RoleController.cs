
using DEPI_PROJECT.BLL.DTOs.Role;
using DEPI_PROJECT.BLL.DTOs.User;
using DEPI_PROJECT.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _roleService.GetAllAsync();
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RoleCreateDto roleCreateDto)
        {
            var response = await _roleService.CreateAsync(roleCreateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(RoleUpdateDto roleUpdateDto)
        {
            var response = await _roleService.UpdateAsync(roleUpdateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return NoContent();
        }

        [HttpDelete("{RoleId}")]
        public async Task<IActionResult> DeleteAsync(Guid RoleId)
        {
            var response = await _roleService.DeleteAsync(RoleId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return NoContent();
        }
    }
}
