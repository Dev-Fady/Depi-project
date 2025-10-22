using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.Manager.PropertyGallery;
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
        private readonly IPropertyGalleryManager _manager;

        public PropertyGalleryController(IPropertyGalleryManager manager)
        {
            _manager = manager;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] PropertyGalleryAddDto dto)
        {
            try
            {
                await _manager.Add(dto);
                return Ok(new { message = $"{dto.MediaFiles.Count} file(s) uploaded successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _manager.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var result = _manager.GetById(id);
                if (result == null)
                    return NotFound(new { message = $"Gallery item with id {id} not found." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("property/{propertyId:guid}")]
        public IActionResult GetByPropertyId(Guid propertyId)
        {
            try
            {
                var result = _manager.GetByPropertyId(propertyId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var deleted = _manager.Delete(id);
                if (!deleted)
                    return NotFound(new { message = $"Gallery item with id {id} not found." });

                return Ok(new { message = "Gallery deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
