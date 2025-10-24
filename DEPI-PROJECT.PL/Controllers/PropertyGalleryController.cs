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
            var response = await _manager.Add(dto);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _manager.GetAll();
            return Ok(response);
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var response = _manager.GetById(id);
            return Ok(response);
        }
        [HttpGet("property/{propertyId:guid}")]
        public IActionResult GetByPropertyId(Guid propertyId)
        {
            var response = _manager.GetByPropertyId(propertyId);
            return Ok(response);
        }
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var response = _manager.Delete(id);
            return Ok(response);
        }
    }
}
