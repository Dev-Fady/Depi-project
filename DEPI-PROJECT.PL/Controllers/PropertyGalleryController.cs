using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertyGalleryController : ControllerBase
    {
        private readonly IPropertyGalleryService _propertyGalleryService;

        public PropertyGalleryController(IPropertyGalleryService propertyGalleryService)
        {
            _propertyGalleryService = propertyGalleryService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public async Task<IActionResult> Upload([FromForm] PropertyGalleryAddDto dto)
        {
            var response = await _propertyGalleryService.AddAsync(dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<PropertyGalleryReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAll()
        {
            var response = await _propertyGalleryService.GetAllAsync();
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ResponseDto<PropertyGalleryReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _propertyGalleryService.GetByIdAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("property/{propertyId:guid}")]
        [ProducesResponseType(typeof(ResponseDto<IEnumerable<PropertyGallery>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetByPropertyId(Guid propertyId)
        {
            var response = await _propertyGalleryService.GetByPropertyIdAsync(propertyId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _propertyGalleryService.DeleteAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
