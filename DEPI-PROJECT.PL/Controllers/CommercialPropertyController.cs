using Azure;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommercialPropertyController : ControllerBase
    {
        private readonly ICommercialPropertyService _commercialPropertyService;

        public CommercialPropertyController(ICommercialPropertyService commercialPropertyService)
        {
            _commercialPropertyService = commercialPropertyService;
        }
        /// <summary>
        /// Get all commercial properties of all agents, you can filter by the agent by setting the userId in the request body
        /// </summary>
        /// <param name="commercialPropertyQueryDto"></param>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<CommercialPropertyReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] CommercialPropertyQueryDto commercialPropertyQueryDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var response = await _commercialPropertyService.GetAllPropertiesAsync(UserId, commercialPropertyQueryDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDto<CommercialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var response = await _commercialPropertyService.GetPropertyByIdAsync(UserId, id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(ResponseDto<CommercialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCommercialProperty([FromBody] CommercialPropertyAddDto propertyDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var AgentId = GetAgentId.GetAgentIdFromUserId(this, UserId);
            var response = await _commercialPropertyService.AddPropertyAsync(UserId, AgentId, propertyDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCommercialProperty(Guid id)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var response = await _commercialPropertyService.DeleteCommercialPropertyAsync(UserId, id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCommercialProperty(Guid id, [FromBody] CommercialPropertyUpdateDto propertyDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var response = await _commercialPropertyService.UpdateCommercialPropertyAsync(UserId, id, propertyDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
