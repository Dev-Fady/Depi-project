using DEPI_PROJECT.BLL.Dtos.Like;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LikeController : ControllerBase
    {
        private readonly ILikePropertyService _likeProperty;
        private readonly ILikeCommentService _likeComment;

        public LikeController(ILikeCommentService likeComment , ILikePropertyService likeProperty)
        {
            _likeProperty = likeProperty;
            _likeComment = likeComment;
        }

        /// <summary>
        /// Toggles the like status for a property (like/unlike)
        /// </summary>
        /// <param name="propertyId">The unique identifier of the property to like/unlike</param>
        /// <returns>Toggle result indicating whether property was liked or unliked</returns>
        /// <response code="200">Returns the toggle result (liked/unliked)</response>
        /// <response code="400">If the property is not found or request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost("ToggleLikeProperty/{propertyId}")]
        [ProducesResponseType(typeof(ResponseDto<ToggleResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> TogglePropertyLike(Guid propertyId)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var result = await _likeProperty.ToggleLikeProperty(UserId, propertyId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        /// <summary>
        /// Toggles the like status for a comment (like/unlike)
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment to like/unlike</param>
        /// <returns>Toggle result indicating whether comment was liked or unliked</returns>
        /// <response code="200">Returns the toggle result (liked/unliked)</response>
        /// <response code="400">If the comment is not found or request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost("ToggleLikeComment/{commentId}")]
        [ProducesResponseType(typeof(ResponseDto<ToggleResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ToggleCommentLike(Guid commentId)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var result = await _likeComment.ToggleLikeComment(UserId, commentId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
