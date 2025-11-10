using Azure;
using DEPI_PROJECT.BLL.Dtos.Comment;
using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.PL.Helper_Function;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentController(ICommentService CommentService) 
        { 
            _service = CommentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<CommentGetDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetAllComments([FromQuery] CommentQueryDto queryDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var Response = await _service.GetAllCommentsByPropertyId(UserId ,queryDto);
            if (!Response.IsSuccess)
            {
                return BadRequest(Response);
            }
            return Ok(Response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseDto<CommentGetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var Response = await _service.GetCommentById(UserId , id);
            if (!Response.IsSuccess)
            {
                return BadRequest(Response);
            }
            return Ok(Response);
        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<CommentGetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CreateComment([FromBody]CommentAddDto createCommentDto)
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
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateComment(Guid id,[FromBody] CommentUpdateDto updateCommentDto)
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
