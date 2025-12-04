using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// Uploads images to a property's gallery (Admin or Agent only)
        /// </summary>
        /// <param name="dto">Property gallery upload details including property ID and image files</param>
        /// <returns>Success message with uploaded file information</returns>
        /// <response code="200">Returns success message with file details</response>
        /// <response code="400">If the upload data is invalid or upload fails</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin or Agent role required)</response>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(ResponseDto<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,AGENT")]
        public async Task<IActionResult> Upload([FromForm] PropertyGalleryAddDto dto)
        {
            Guid UserId = GetUserIdFromToken.GetCurrentUserId(this);   
            var response = await _propertyGalleryService.AddAsync(UserId, dto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        /// <summary>
        /// Retrieves all property gallery images
        /// </summary>
        /// <returns>List of all property gallery images</returns>
        /// <response code="200">Returns the list of property gallery images</response>
        /// <response code="400">If the request fails</response>
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

        /// <summary>
        /// Retrieves a specific property gallery image by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the property gallery image</param>
        /// <returns>Property gallery image details if found</returns>
        /// <response code="200">Returns the property gallery image details</response>
        /// <response code="400">If the image is not found or request is invalid</response>
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

        /// <summary>
        /// Retrieves all gallery images for a specific property
        /// </summary>
        /// <param name="propertyId">The unique identifier of the property</param>
        /// <returns>List of gallery images for the specified property</returns>
        /// <response code="200">Returns the property's gallery images</response>
        /// <response code="400">If the property is not found or request is invalid</response>
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

        /// <summary>
        /// Deletes a property gallery image (Admin or Property Owner Agent only)
        /// </summary>
        /// <param name="id">The unique identifier of the property gallery image to delete</param>
        /// <returns>Success status of the delete operation</returns>
        /// <response code="200">Returns success if image is deleted</response>
        /// <response code="400">If the image is not found or user is not authorized to delete</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <response code="403">If the user is not authorized (Admin or Property Owner Agent role required)</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,AGENT")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var response = await _propertyGalleryService.DeleteAsync(UserId, id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
