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

        /// <summary>
        /// Retrieves a specific commercial property by its ID with like information for the current user
        /// </summary>
        /// <param name="id">The unique identifier of the commercial property</param>
        /// <returns>Commercial property details with like count and user's like status</returns>
        /// <response code="200">Returns the commercial property details</response>
        /// <response code="400">If the property is not found or request is invalid</response>
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

        /// <summary>
        /// Creates a new commercial property listing (Agent only)
        /// </summary>
        /// <param name="propertyDto">Commercial property details including business type, floor number, and amenities</param>
        /// <returns>Created commercial property details</returns>
        /// <response code="200">Returns the newly created commercial property</response>
        /// <response code="400">If the property data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Agent role required)</response>
        [HttpPost("")]
        [ProducesResponseType(typeof(ResponseDto<CommercialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> AddCommercialProperty([FromBody] CommercialPropertyAddDto propertyDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var response = await _commercialPropertyService.AddPropertyAsync(UserId, propertyDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a commercial property listing (Admin or Property Owner Agent only)
        /// </summary>
        /// <param name="id">The unique identifier of the commercial property to delete</param>
        /// <returns>Success status of the delete operation</returns>
        /// <response code="200">Returns success if property is deleted</response>
        /// <response code="400">If the property is not found or user is not authorized to delete</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin or Property Owner Agent role required)</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,AGENT")]
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

        /// <summary>
        /// Updates an existing commercial property's information (Admin or Property Owner Agent only)
        /// </summary>
        /// <param name="id">The unique identifier of the commercial property to update</param>
        /// <param name="propertyDto">Updated commercial property details</param>
        /// <returns>Success status of the update operation</returns>
        /// <response code="200">Returns success if property is updated</response>
        /// <response code="400">If the update data is invalid or user is not authorized to update</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin or Property Owner Agent role required)</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,AGENT")]
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
