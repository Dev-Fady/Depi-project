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

        /// <summary>
        /// Retrieves all residential properties with filtering, pagination, and like information for the current user
        /// </summary>
        /// <param name="queryDto">Query parameters for filtering by bedrooms, bathrooms, kitchen type, etc.</param>
        /// <returns>Paginated list of residential properties with like counts and user's like status</returns>
        /// <response code="200">Returns the paginated list of residential properties</response>
        /// <response code="400">If the request parameters are invalid</response>
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

        /// <summary>
        /// Retrieves a specific residential property by its ID with like information for the current user
        /// </summary>
        /// <param name="id">The unique identifier of the residential property</param>
        /// <returns>Residential property details with like count and user's like status</returns>
        /// <response code="200">Returns the residential property details</response>
        /// <response code="400">If the property is not found or request is invalid</response>
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

        /// <summary>
        /// Creates a new residential property listing (Agent only)
        /// </summary>
        /// <param name="propertyDto">Residential property details including bedrooms, bathrooms, floors, and amenities</param>
        /// <returns>Created residential property details</returns>
        /// <response code="200">Returns the newly created residential property</response>
        /// <response code="400">If the property data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Agent role required)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<ResidentialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "AGENT")]
        public async Task<IActionResult> AddResidentialProperty([FromBody] ResidentialPropertyAddDto propertyDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var response = await _residentialPropertyService.AddResidentialPropertyAsync(UserId, propertyDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing residential property's information (Admin or Property Owner Agent only)
        /// </summary>
        /// <param name="id">The unique identifier of the residential property to update</param>
        /// <param name="propertyDto">Updated residential property details</param>
        /// <returns>Success status of the update operation</returns>
        /// <response code="200">Returns success if property is updated</response>
        /// <response code="400">If the update data is invalid or user is not authorized to update</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin or Property Owner Agent role required)</response>
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

        /// <summary>
        /// Deletes a residential property listing (Admin or Property Owner Agent only)
        /// </summary>
        /// <param name="id">The unique identifier of the residential property to delete</param>
        /// <returns>Success status of the delete operation</returns>
        /// <response code="200">Returns success if property is deleted</response>
        /// <response code="400">If the property is not found or user is not authorized to delete</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin or Property Owner Agent role required)</response>
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
