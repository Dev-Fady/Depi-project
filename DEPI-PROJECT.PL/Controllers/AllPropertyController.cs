using DEPI_PROJECT.BLL.Services.Implements;
using DEPI_PROJECT.BLL.Services.Interfaces;
using DEPI_PROJECT.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPI_PROJECT.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllPropertyController : ControllerBase
    {
        private readonly ICommercialPropertyService _commercialService;
        private readonly IResidentialPropertyService _residentialService;

        public AllPropertyController(
            ICommercialPropertyService commercialService,
            IResidentialPropertyService residentialService)
        {
            _commercialService = commercialService;
            _residentialService = residentialService;
        }

        [HttpGet("GetAllProperties")]
        // [Authorize(Roles = "ADMIN")]
        public IActionResult GetAllProperties([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var commercial = _commercialService.GetAllProperties(pageNumber, pageSize);
            var residential = _residentialService.GetAllResidentialProperty(pageNumber, pageSize);

            var allProperties = commercial.Data.Data
                                  .Cast<object>()
                                  .Concat(residential.Data.Data.Cast<object>())
                                  .ToList();
            int totalCount = commercial.Data.TotalCount + residential.Data.TotalCount;
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            bool isNextPage = pageNumber < totalPages;

            var result = new
            {
                TotalCommercial = commercial.Data.TotalCount,
                TotalResidential = residential.Data.TotalCount,
                TotalAll = totalCount,
                TotalPage = totalPages,
                IsNextPage = isNextPage,
                Properties = allProperties
            };

            return Ok(result);
        }

    }

}
