using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentialPropertyController : ControllerBase
    {
        private readonly IResidentialPropertyService _residentialPropertyService;
        public ResidentialPropertyController(IResidentialPropertyService residentialPropertyService)
        {
            _residentialPropertyService = residentialPropertyService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<ResidentialPropertyReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAllResidentialProperty([FromQuery] ResidentialPropertyQueryDto queryDto)
        {
            // Get Current User Id from Token-->new
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);

            var response = await _residentialPropertyService.GetAllResidentialPropertyAsync(UserId, queryDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDto<ResidentialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var response = await _residentialPropertyService.GetResidentialPropertyByIdAsync(UserId,id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<ResidentialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> AddResidentialProperty([FromBody] ResidentialPropertyAddDto propertyDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var AgentId = GetAgentId.GetAgentIdFromUserId(this, UserId);
            var response = await _residentialPropertyService.AddResidentialPropertyAsync(UserId, AgentId, propertyDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,AGENT")]
        public async Task<IActionResult> UpdateResidentialProperty(Guid id, [FromBody] ResidentialPropertyUpdateDto propertyDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var response = await _residentialPropertyService.UpdateResidentialPropertyAsync(UserId, id, propertyDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,AGENT")]
        public async Task<IActionResult> DeleteResidentialProperty(Guid id)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var response = await _residentialPropertyService.DeleteResidentialPropertyAsync(UserId, id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
