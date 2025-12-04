using Azure;
using DEPI_PROJECT.BLL.Dtos.Comment;
using DEPI_PROJECT.BLL.DTOs.Agent;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Retrieves all comments for a specific property with pagination and like information for the current user
        /// </summary>
        /// <param name="PropertyId">The unique identifier of the property to get comments for</param>
        /// <param name="queryDto">Query parameters for pagination and filtering comments</param>
        /// <returns>Paginated list of comments with like counts and user's like status</returns>
        /// <response code="200">Returns the paginated list of comments for the property</response>
        /// <response code="400">If the property is not found or request parameters are invalid</response>
        [HttpGet("property/{PropertyId}")]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<CommentGetDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllComments(Guid PropertyId, [FromQuery] CommentQueryDto queryDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var Response = await _service.GetAllCommentsByPropertyId(UserId ,PropertyId, queryDto);
            if (!Response.IsSuccess)
            {
                return BadRequest(Response);
            }
            return Ok(Response);
        }

        /// <summary>
        /// Retrieves a specific comment by its ID with like information for the current user
        /// </summary>
        /// <param name="id">The unique identifier of the comment</param>
        /// <returns>Comment details with like count and user's like status</returns>
        /// <response code="200">Returns the comment details</response>
        /// <response code="400">If the comment is not found or request is invalid</response>
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


        /// <summary>
        /// Creates a new comment on a property (Authenticated users only)
        /// </summary>
        /// <param name="createCommentDto">Comment details including property ID and comment text</param>
        /// <returns>Created comment details</returns>
        /// <response code="200">Returns the newly created comment</response>
        /// <response code="400">If the comment data is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<CommentGetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize]
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

        /// <summary>
        /// Updates an existing comment (Comment owner or Admin only)
        /// </summary>
        /// <param name="id">The unique identifier of the comment to update</param>
        /// <param name="updateCommentDto">Updated comment details</param>
        /// <returns>Success status of the update operation</returns>
        /// <response code="200">Returns success if comment is updated</response>
        /// <response code="400">If the update data is invalid or user is not authorized to update</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize]
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

        /// <summary>
        /// Deletes a comment (Comment owner or Admin only)
        /// </summary>
        /// <param name="id">The unique identifier of the comment to delete</param>
        /// <returns>Success status of the delete operation</returns>
        /// <response code="200">Returns success if comment is deleted</response>
        /// <response code="400">If the comment is not found or user is not authorized to delete</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        [Authorize]
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

        /// <summary>
        /// Gets the total count of comments for a specific property
        /// </summary>
        /// <param name="PropertyId">The unique identifier of the property to count comments for</param>
        /// <returns>Total number of comments on the property</returns>
        /// <response code="200">Returns the comment count</response>
        /// <response code="400">If the property is not found or request is invalid</response>
        [HttpGet("Count/Property/{PropertyId}")]
        [ProducesResponseType(typeof(ResponseDto<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CountAllComment(Guid PropertyId)
        {
            var Response = await _service.CountAllComments(PropertyId);
            if (!Response.IsSuccess)
            {
                return BadRequest(Response);
            }
            return Ok(Response);
        }
    }
}
