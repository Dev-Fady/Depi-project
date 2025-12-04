using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Retrieves all agents with pagination and filtering options
        /// </summary>
        /// <param name="agentQueryDto">Query parameters for filtering and pagination</param>
        /// <returns>Paginated list of agents</returns>
        /// <response code="200">Returns the paginated list of agents</response>
        /// <response code="400">If the request is invalid</response>
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

        /// <summary>
        /// Retrieves a specific agent by their user ID
        /// </summary>
        /// <param name="UserId">The unique identifier of the user associated with the agent</param>
        /// <returns>Agent details if found</returns>
        /// <response code="200">Returns the agent details</response>
        /// <response code="400">If the agent is not found or request is invalid</response>
        [HttpGet("{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<AgentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync(Guid UserId)
        {
            var response = await _agentService.GetByIdAsync(UserId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Creates a new agent record (Admin only)
        /// </summary>
        /// <param name="agentCreateDto">Agent details for creation</param>
        /// <returns>Created agent details</returns>
        /// <response code="200">Returns the newly created agent</response>
        /// <response code="400">If the agent data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<AgentResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateAsync(AgentCreateDto agentCreateDto)
        {
            var response = await _agentService.CreateAsync(agentCreateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing agent's information (Admin or Agent only)
        /// </summary>
        /// <param name="agentUpdateDto">Updated agent details</param>
        /// <returns>Success status of the update operation</returns>
        /// <response code="200">Returns success if agent is updated</response>
        /// <response code="400">If the update data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin or Agent role required)</response>
        [HttpPut]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,AGENT")]
        public async Task<IActionResult> UpdateAsync(AgentUpdateDto agentUpdateDto)
        {
            var response = await _agentService.UpdateAsync(agentUpdateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes an agent record (Admin or Agent only)
        /// </summary>
        /// <param name="UserId">The unique identifier of the user associated with the agent to delete</param>
        /// <returns>Success status of the delete operation</returns>
        /// <response code="200">Returns success if agent is deleted</response>
        /// <response code="400">If the agent is not found or request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin or Agent role required)</response>
        [HttpDelete("{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,AGENT")]
        public async Task<IActionResult> DeleteAsync(Guid UserId)
        {
            var response = await _agentService.DeleteAsync(UserId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}