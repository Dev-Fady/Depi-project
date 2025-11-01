using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var response = await _residentialPropertyService.GetAllResidentialPropertyAsync(queryDto);
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
            var response = await _residentialPropertyService.GetResidentialPropertyByIdAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<ResidentialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public async Task<IActionResult> AddResidentialProperty([FromBody] ResidentialPropertyAddDto propertyDto)
        {
            var response = await _residentialPropertyService.AddResidentialPropertyAsync(propertyDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public async Task<IActionResult> UpdateResidentialProperty(Guid id, [FromBody] ResidentialPropertyUpdateDto propertyDto)
        {
            var response = await _residentialPropertyService.UpdateResidentialPropertyAsync(id, propertyDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public async Task<IActionResult> DeleteResidentialProperty(Guid id)
        {
            var response = await _residentialPropertyService.DeleteResidentialPropertyAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
