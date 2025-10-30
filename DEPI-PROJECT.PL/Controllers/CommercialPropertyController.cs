using Azure;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
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
    public class CommercialPropertyController : ControllerBase
    {
        private readonly ICommercialPropertyService _commercialPropertyService;

        public CommercialPropertyController(ICommercialPropertyService commercialPropertyService)
        {
            _commercialPropertyService = commercialPropertyService;
        }

        [HttpGet("GetAllProperties")]
        [ProducesResponseType(typeof(ResponseDto<PagedResult<CommercialPropertyReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public IActionResult GetAllProperties([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = _commercialPropertyService.GetAllProperties(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(ResponseDto<CommercialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public IActionResult GetById(Guid id)
        {
            var result = _commercialPropertyService.GetPropertyById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("AddCommercialProperty")]
        [ProducesResponseType(typeof(ResponseDto<CommercialPropertyReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public IActionResult AddCommercialProperty([FromBody] CommercialPropertyAddDto propertyDto)
        {
            var response = _commercialPropertyService.AddProperty(propertyDto);
            return Ok(response);
        }

        [HttpDelete("DeleteCommercialProperty/{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCommercialProperty(Guid id)
        {
            var response = _commercialPropertyService.DeleteCommercialProperty(id);
            return Ok(response);
        }

        [HttpPut("UpdateCommercialProperty/{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCommercialProperty(Guid id, [FromBody] CommercialPropertyUpdateDto propertyDto)
        {
            var response = _commercialPropertyService.UpdateCommercialProperty(id, propertyDto);
            return Ok(response);
        }
    }
}
