using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Implements;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        /// <summary>
        /// Retrieves all properties (both commercial and residential) with filtering and pagination
        /// </summary>
        /// <param name="propertyQueryDto">Query parameters for filtering properties by city, type, price range, etc.</param>
        /// <returns>Paginated list containing both commercial and residential properties</returns>
        /// <response code="200">Returns the paginated list of all properties</response>
        /// <response code="400">If the request parameters are invalid</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<AllPropertyReadDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] PropertyQueryDto propertyQueryDto)
        {
            var result = await _propertyService.GetAll(propertyQueryDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }

}
