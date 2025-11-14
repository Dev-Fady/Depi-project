using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<AgentResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] AgentQueryDto agentQueryDto)
        {
            var response = await _agentService.GetAllAsync(agentQueryDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<AgentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync(Guid UserId)
        {
            var AgentId = GetAgentId.GetAgentIdFromUserId(this, UserId);
            var response = await _agentService.GetByIdAsync(AgentId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<AgentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(AgentCreateDto agentCreateDto)
        {
            var response = await _agentService.CreateAsync(agentCreateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsync(AgentUpdateDto agentUpdateDto)
        {
            var AgentId = GetAgentId.GetAgentIdFromUserId(this, agentUpdateDto.UserId);
            var response = await _agentService.UpdateAsync(AgentId, agentUpdateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(Guid UserId)
        {
            var AgentId = GetAgentId.GetAgentIdFromUserId(this, UserId);
            var response = await _agentService.DeleteAsync(UserId, AgentId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}