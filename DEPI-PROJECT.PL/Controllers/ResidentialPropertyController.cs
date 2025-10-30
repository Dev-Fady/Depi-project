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

        [HttpGet("GetAllResidentialProperty")]
        [ProducesResponseType(typeof(ResponseDto<PagedResult<ResidentialPropertyReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public IActionResult GetAllResidentialProperty([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = _residentialPropertyService.GetAllResidentialProperty(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(ResponseDto<ResidentialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public IActionResult GetById(Guid id)
        {
            var result = _residentialPropertyService.GetResidentialPropertyById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("AddResidentialProperty")]
        [ProducesResponseType(typeof(ResponseDto<ResidentialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public IActionResult AddResidentialProperty([FromBody] ResidentialPropertyAddDto propertyDto)
        {
            var response = _residentialPropertyService.AddResidentialProperty(propertyDto);
            return Ok(response);
        }

        [HttpPut("UpdateResidentialProperty/{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public IActionResult UpdateResidentialProperty(Guid id, [FromBody] ResidentialPropertyUpdateDto propertyDto)
        {
            var response = _residentialPropertyService.UpdateResidentialProperty(id, propertyDto);
            return Ok(response);
        }

        [HttpDelete("DeleteResidentialProperty/{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public IActionResult DeleteResidentialProperty(Guid id)
        {
            var response = _residentialPropertyService.DeleteResidentialProperty(id);
            return Ok(response);
        }
    }
}
