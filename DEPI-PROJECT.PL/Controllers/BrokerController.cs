using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.Broker;
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrokerController : ControllerBase
    {
        private readonly IBrokerService _brokerService;
        public BrokerController(IBrokerService brokerService)
        {
            _brokerService = brokerService;
        }

        /// <summary>
        /// Retrieves all brokers with pagination and filtering options
        /// </summary>
        /// <param name="brokerQueryDto">Query parameters for filtering and pagination</param>
        /// <returns>Paginated list of brokers</returns>
        /// <response code="200">Returns the paginated list of brokers</response>
        /// <response code="400">If the request parameters are invalid</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<BrokerResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] BrokerQueryDto brokerQueryDto)
        {
            var response = await _brokerService.GetAllAsync(brokerQueryDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific broker by their user ID
        /// </summary>
        /// <param name="UserId">The unique identifier of the user associated with the broker</param>
        /// <returns>Broker details if found</returns>
        /// <response code="200">Returns the broker details</response>
        /// <response code="400">If the broker is not found or request is invalid</response>
        [HttpGet("{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<BrokerResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync(Guid UserId)
        {
            var response = await _brokerService.GetByIdAsync(UserId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Creates a new broker record (Admin only)
        /// </summary>
        /// <param name="brokerCreateDto">Broker details for creation</param>
        /// <returns>Created broker details</returns>
        /// <response code="200">Returns the newly created broker</response>
        /// <response code="400">If the broker data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<BrokerResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateAsync(BrokerCreateDto brokerCreateDto)
        {
            var response = await _brokerService.CreateAsync(brokerCreateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing broker's information (Admin or Broker only)
        /// </summary>
        /// <param name="brokerUpdateDto">Updated broker details</param>
        /// <returns>Success status of the update operation</returns>
        /// <response code="200">Returns success if broker is updated</response>
        /// <response code="400">If the update data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin or Broker role required)</response>
        [HttpPut]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,BROKER")]
        public async Task<IActionResult> UpdateAsync(BrokerUpdateDto brokerUpdateDto)
        {
            var response = await _brokerService.UpdateAsync(brokerUpdateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a broker record (Admin or Broker only)
        /// </summary>
        /// <param name="UserId">The unique identifier of the user associated with the broker to delete</param>
        /// <returns>Success status of the delete operation</returns>
        /// <response code="200">Returns success if broker is deleted</response>
        /// <response code="400">If the broker is not found or request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin or Broker role required)</response>
        [HttpDelete("{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,BROKER")]
        public async Task<IActionResult> DeleteAsync(Guid UserId)
        {
            var response = await _brokerService.DeleteAsync(UserId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}