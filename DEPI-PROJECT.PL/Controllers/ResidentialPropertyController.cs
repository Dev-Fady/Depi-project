using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.BLL.Manager.ResidentialProperty;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentialPropertyController : ControllerBase
    {
        private readonly IResidentialPropertyManager _residentialPropertyManager;
        public ResidentialPropertyController(IResidentialPropertyManager residentialPropertyManager)
        {
            _residentialPropertyManager = residentialPropertyManager;
        }

        [HttpGet("GetAllResidentialProperty")]
        public IActionResult GetAllResidentialProperty([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = _residentialPropertyManager.GetAllResidentialProperty(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _residentialPropertyManager.GetResidentialPropertyById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost("AddResidentialProperty")]
        public IActionResult AddResidentialProperty([FromBody] ResidentialPropertyAddDto propertyDto)
        {
            var createdProperty = _residentialPropertyManager.AddResidentialProperty(propertyDto);
            return CreatedAtAction(
                nameof(GetById),
                 new { id = createdProperty.PropertyId },
                propertyDto);
        }
        [HttpDelete("DeleteResidentialProperty/{id}")]
        public IActionResult DeleteResidentialProperty(Guid id)
        {
            var success = _residentialPropertyManager.DeleteResidentialProperty(id);
            if (!success) return NotFound();
            return Ok("Deleted successfully");
        }
        [HttpPut("UpdateResidentialProperty/{id}")]
        public IActionResult UpdateResidentialProperty(Guid id, [FromBody] ResidentialPropertyUpdateDto propertyDto)
        {
            var success = _residentialPropertyManager.UpdateResidentialProperty(id, propertyDto);
            if (!success) return NotFound();
            return Ok("Updated successfully");
        }
    }
}
