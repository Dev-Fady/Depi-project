using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyGalleryController : ControllerBase
    {
        private readonly IPropertyGalleryService _propertyGalleryService;

        public PropertyGalleryController(IPropertyGalleryService propertyGalleryService)
        {
            _propertyGalleryService = propertyGalleryService;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public async Task<IActionResult> Upload([FromForm] PropertyGalleryAddDto dto)
        {
            var response = await _propertyGalleryService.Add(dto);
            return Ok(response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<PropertyGalleryReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public IActionResult GetAll()
        {
            var response = _propertyGalleryService.GetAll();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ResponseDto<PropertyGalleryReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public IActionResult GetById(Guid id)
        {
            var response = _propertyGalleryService.GetById(id);
            return Ok(response);
        }

        [HttpGet("property/{propertyId:guid}")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<PropertyGallery>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public IActionResult GetByPropertyId(Guid propertyId)
        {
            var response = _propertyGalleryService.GetByPropertyId(propertyId);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public IActionResult Delete(Guid id)
        {
            var response = _propertyGalleryService.Delete(id);
            return Ok(response);
        }
    }
}
