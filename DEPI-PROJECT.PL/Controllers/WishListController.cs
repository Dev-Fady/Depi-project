using DEPI_PROJECT.BLL.Dtos.Wishists;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Repositories.Interfaces;
using DEPI_PROJECT.PL.Helper_Function;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _wishListService;
        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }


        /// <summary>
        /// Retrieves all items in the user's wishlist with pagination (Authenticated users only)
        /// </summary>
        /// <param name="queryDto">Query parameters for pagination</param>
        /// <returns>Paginated list of user's wishlist items</returns>
        /// <response code="200">Returns the user's wishlist items</response>
        /// <response code="400">If the request parameters are invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<WishListGetDto?>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllWishList([FromQuery] WishListQueryDto queryDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var result = await _wishListService.GetAllWishList(UserId, queryDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Adds a property to the user's wishlist (Authenticated users only)
        /// </summary>
        /// <param name="wishListAddDto">Wishlist item details including property ID</param>
        /// <returns>Added wishlist item details</returns>
        /// <response code="200">Returns the added wishlist item</response>
        /// <response code="400">If the item data is invalid or property is already in wishlist</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<WishListGetDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddItemInWishList([FromBody] WishListAddDto wishListAddDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var result = await _wishListService.AddItemInWishList(UserId, wishListAddDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Removes a property from the user's wishlist (Authenticated users only)
        /// </summary>
        /// <param name="wishListDeleteDto">Wishlist item removal details including property ID</param>
        /// <returns>Success status of the removal operation</returns>
        /// <response code="200">Returns success if item is removed from wishlist</response>
        /// <response code="400">If the item is not found in wishlist or request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpDelete]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteItemInWishList([FromBody] WishListDeleteDto wishListDeleteDto)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var result = await _wishListService.DeleteItemInWishList(UserId, wishListDeleteDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Checks if a specific property is in the user's wishlist (Authenticated users only)
        /// </summary>
        /// <param name="PropertyId">The unique identifier of the property to check</param>
        /// <returns>Wishlist item details if property is in user's wishlist</returns>
        /// <response code="200">Returns wishlist item details or null if not in wishlist</response>
        /// <response code="400">If the property is not found or request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        [HttpGet("{PropertyId}")]
        [ProducesResponseType(typeof(ResponseDto<WishListGetDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetWishList(Guid PropertyId)
        {
            var UserId = GetUserIdFromToken.GetCurrentUserId(this);
            var result = await _wishListService.GetWishList(UserId, PropertyId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
