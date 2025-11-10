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
