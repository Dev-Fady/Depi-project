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
        private readonly IResidentialPropertyManager _manager;
        public ResidentialPropertyController(IResidentialPropertyManager residentialPropertyManager)
        {
            _manager = residentialPropertyManager;
        }

        [HttpGet("GetAllResidentialProperty")]
        public IActionResult GetAllResidentialProperty([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = _manager.GetAllResidentialProperty(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _manager.GetResidentialPropertyById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost("AddResidentialProperty")]
        public IActionResult AddResidentialProperty([FromBody] ResidentialPropertyAddDto propertyDto)
        {
            var response = _manager.AddResidentialProperty(propertyDto);
            return Ok(response);
        }

        [HttpPut("UpdateResidentialProperty/{id}")]
        public IActionResult UpdateResidentialProperty(Guid id, [FromBody] ResidentialPropertyUpdateDto propertyDto)
        {
            var response = _manager.UpdateResidentialProperty(id, propertyDto);
            return Ok(response);
        }

        [HttpDelete("DeleteResidentialProperty/{id}")]
        public IActionResult DeleteResidentialProperty(Guid id)
        {
            var response = _manager.DeleteResidentialProperty(id);
            return Ok(response);
        }
    }
}
