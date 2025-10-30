using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
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
        public IActionResult GetAllResidentialProperty([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = _residentialPropertyService.GetAllResidentialProperty(pageNumber, pageSize);
            return Ok(result);
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _residentialPropertyService.GetResidentialPropertyById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost("AddResidentialProperty")]
        public IActionResult AddResidentialProperty([FromBody] ResidentialPropertyAddDto propertyDto)
        {
            var response = _residentialPropertyService.AddResidentialProperty(propertyDto);
            return Ok(response);
        }

        [HttpPut("UpdateResidentialProperty/{id}")]
        public IActionResult UpdateResidentialProperty(Guid id, [FromBody] ResidentialPropertyUpdateDto propertyDto)
        {
            var response = _residentialPropertyService.UpdateResidentialProperty(id, propertyDto);
            return Ok(response);
        }

        [HttpDelete("DeleteResidentialProperty/{id}")]
        public IActionResult DeleteResidentialProperty(Guid id)
        {
            var response = _residentialPropertyService.DeleteResidentialProperty(id);
            return Ok(response);
        }
    }
}
