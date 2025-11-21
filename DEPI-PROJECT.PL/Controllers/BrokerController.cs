using System.Threading.Tasks;
using DEPI_PROJECT.BLL.DTOs.Broker;
using DEPI_PROJECT.BLL.DTOs.Authentication;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrokerController : ControllerBase
    {
        private readonly IBrokerService _brokerService;
        public BrokerController(IBrokerService brokerService)
        {
            _brokerService = brokerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseDto<PagedResultDto<BrokerResponseDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAsync([FromQuery] BrokerQueryDto brokerQueryDto)
        {
            var response = await _brokerService.GetAllAsync(brokerQueryDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<BrokerResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByIdAsync(Guid UserId)
        {
            var response = await _brokerService.GetByIdAsync(UserId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseDto<BrokerResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateAsync(BrokerCreateDto brokerCreateDto)
        {
            var response = await _brokerService.CreateAsync(brokerCreateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,BROKER")]
        public async Task<IActionResult> UpdateAsync(BrokerUpdateDto brokerUpdateDto)
        {
            var response = await _brokerService.UpdateAsync(brokerUpdateDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{UserId}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "ADMIN,BROKER")]
        public async Task<IActionResult> DeleteAsync(Guid UserId)
        {
            var response = await _brokerService.DeleteAsync(UserId);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}