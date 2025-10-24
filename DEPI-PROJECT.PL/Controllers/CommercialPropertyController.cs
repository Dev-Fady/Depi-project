using Azure;
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.Manager.CommercialProperty;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommercialPropertyController : ControllerBase
    {
        private readonly ICommercialPropertyManager _mangaer;

        public CommercialPropertyController(ICommercialPropertyManager mangaer)
        {
            _mangaer = mangaer;
        }
        [HttpGet("GetAllProperties")]
        public IActionResult GetAllProperties([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = _mangaer.GetAllProperties(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _mangaer.GetPropertyById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost("AddCommercialProperty")]
        public IActionResult AddCommercialProperty([FromBody] CommercialPropertyAddDto propertyDto)
        {
            var response = _mangaer.AddProperty(propertyDto);
            return Ok(response);
        }

        [HttpDelete("DeleteCommercialProperty/{id}")]
        public IActionResult DeleteCommercialProperty(Guid id)
        {
            var response = _mangaer.DeleteCommercialProperty(id);
            return Ok(response);
        }
        [HttpPut("UpdateCommercialProperty/{id}")]
        public IActionResult UpdateCommercialProperty(Guid id, [FromBody] CommercialPropertyUpdateDto propertyDto)
        {
            var response = _mangaer.UpdateCommercialProperty(id, propertyDto);
            return Ok(response);
        }
    }
}
