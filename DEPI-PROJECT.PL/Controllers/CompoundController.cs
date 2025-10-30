using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.Compound;
using DEPI_PROJECT.BLL.DTOs.Pagination;
using DEPI_PROJECT.BLL.DTOs.Response;
using DEPI_PROJECT.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompoundController : ControllerBase
    {
        private readonly ICompoundService _compundService;

        public CompoundController(ICompoundService compoundService)
        {
            _compundService = compoundService;
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(ResponseDto<PagedResult<CompoundReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public IActionResult GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var res = _compundService.GetAllCompounds(pageNumber, pageSize);
            return Ok(res);
        }

        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(ResponseDto<CompoundReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        public IActionResult GetById(Guid id)
        {
            var result = _compundService.GetCompoundById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("AddCompound")]
        [ProducesResponseType(typeof(ResponseDto<CompoundReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public IActionResult AddCompound([FromBody] CompoundAddDto Dto)
        {
            var response = _compundService.AddCompound(Dto);
            return Ok(response);
        }

        [HttpDelete("DeleteCompound/{id}")]

        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]

        public IActionResult DeleteCompound(Guid id)
        {
            var response = _compundService.DeleteCompound(id);
            return Ok(response);
        }

        [HttpPut("UpdateCompound/{id}")]
        [ProducesResponseType(typeof(ResponseDto<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseDto<object>), StatusCodes.Status400BadRequest)]
        
        public IActionResult UpdateCompound(Guid id, [FromBody] CompoundUpdateDto Dto)
        {
            var response = _compundService.UpdateCompound(id, Dto);
            return Ok(response);
        }

    }

}
