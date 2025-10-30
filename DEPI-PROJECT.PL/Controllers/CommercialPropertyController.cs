using Azure;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.Services.Interfaces;
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
        public IActionResult GetAllProperties([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = _commercialPropertyService.GetAllProperties(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _commercialPropertyService.GetPropertyById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost("AddCommercialProperty")]
        public IActionResult AddCommercialProperty([FromBody] CommercialPropertyAddDto propertyDto)
        {
            var response = _commercialPropertyService.AddProperty(propertyDto);
            return Ok(response);
        }

        [HttpDelete("DeleteCommercialProperty/{id}")]
        public IActionResult DeleteCommercialProperty(Guid id)
        {
            var response = _commercialPropertyService.DeleteCommercialProperty(id);
            return Ok(response);
        }
        [HttpPut("UpdateCommercialProperty/{id}")]
        public IActionResult UpdateCommercialProperty(Guid id, [FromBody] CommercialPropertyUpdateDto propertyDto)
        {
            var response = _commercialPropertyService.UpdateCommercialProperty(id, propertyDto);
            return Ok(response);
        }
    }
}
