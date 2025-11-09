using Azure;
using DEPI_PROJECT.BLL.Dtos.Comment;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DEPI_PROJECT.PL.Helper_Function;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService CommentService) 
        { 
            _service = CommentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<GetCommentDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAllComments([FromQuery] CommentQueryDto queryDto)
        {
            var Response = await _service.GetAllCommentsByPropertyId(queryDto);
            if (!Response.IsSuccess)
            {
                return BadRequest(Response);
            }
            return Ok(Response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDto<GetCommentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var Response = await _service.GetCommentById(id);
            if (!Response.IsSuccess)
            {
                return BadRequest(Response);
            }
            return Ok(Response);
        }


        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ResponseDto<GetCommentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CreateComment([FromBody]AddCommentDto createCommentDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var Response = await _service.AddComment(UserId , createCommentDto);
            if (!Response.IsSuccess)
            {
                return BadRequest(Response);
            }
            return Ok(Response);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateComment(Guid id,[FromBody] UpdateCommentDto updateCommentDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var Response = await _service.UpdateComment(UserId, updateCommentDto , id);
            if (!Response.IsSuccess)
            {
                return BadRequest(Response);
            }
            return Ok(Response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var Response = await _service.DeleteComment(UserId,id);
            if (!Response.IsSuccess)
            {
                return BadRequest(Response);
            }
            return Ok(Response);
        }

        [HttpGet("Count")]
        [ProducesResponseType(typeof(ResponseDto<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CountAllComment([FromQuery] Guid Propertyid)
        {
            var Response = await _service.CountAllComments(Propertyid);
            if (!Response.IsSuccess)
            {
                return BadRequest(Response);
            }
            return Ok(Response);
        }
    }
}
