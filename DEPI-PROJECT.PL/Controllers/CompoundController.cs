using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Compound;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompoundController : ControllerBase
    {
        private readonly ICompoundService _compundService;

        public CompoundController(ICompoundService compoundService)
        {
            _compundService = compoundService;
        }

        /// <summary>
        /// Retrieves all compounds with pagination and filtering options
        /// </summary>
        /// <param name="compoundQueryDto">Query parameters for filtering and pagination</param>
        /// <returns>Paginated list of compounds</returns>
        /// <response code="200">Returns the paginated list of compounds</response>
        /// <response code="400">If the request parameters are invalid</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<CompoundReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] CompoundQueryDto compoundQueryDto)
        {
            var response = await _compundService.GetAllCompoundsAsync(compoundQueryDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific compound by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the compound</param>
        /// <returns>Compound details if found</returns>
        /// <response code="200">Returns the compound details</response>
        /// <response code="400">If the compound is not found or request is invalid</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDto<CompoundReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _compundService.GetCompoundByIdAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Creates a new compound (Admin only)
        /// </summary>
        /// <param name="Dto">Compound details for creation</param>
        /// <returns>Created compound details</returns>
        /// <response code="200">Returns the newly created compound</response>
        /// <response code="400">If the compound data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<CompoundReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddCompound([FromBody] CompoundAddDto Dto)
        {
            var response = await _compundService.AddCompoundAsync(Dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Deletes a compound (Admin only)
        /// </summary>
        /// <param name="id">The unique identifier of the compound to delete</param>
        /// <returns>Success status of the delete operation</returns>
        /// <response code="200">Returns success if compound is deleted</response>
        /// <response code="400">If the compound is not found or request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCompound(Guid id)
        {
            var response = await _compundService.DeleteCompoundAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Updates an existing compound's information (Admin only)
        /// </summary>
        /// <param name="id">The unique identifier of the compound to update</param>
        /// <param name="Dto">Updated compound details</param>
        /// <returns>Success status of the update operation</returns>
        /// <response code="200">Returns success if compound is updated</response>
        /// <response code="400">If the update data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin role required)</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateCompound(Guid id, [FromBody] CompoundUpdateDto Dto)
        {
            var response = await _compundService.UpdateCompoundAsync(id, Dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }

}
