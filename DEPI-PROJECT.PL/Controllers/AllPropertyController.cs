using DEPI_PROJECT.BLL.Manager.CommercialProperty;
using DEPI_PROJECT.BLL.Manager.ResidentialProperty;
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
        private readonly ICommercialPropertyManager _commercialManager;
        private readonly IResidentialPropertyManager _residentialManager;

        public AllPropertyController(
            ICommercialPropertyManager commercialManager,
            IResidentialPropertyManager residentialManager)
        {
            _commercialManager = commercialManager;
            _residentialManager = residentialManager;
        }

        [HttpGet("GetAllProperties")]
        // [Authorize(Roles = "ADMIN")]
        public IActionResult GetAllProperties([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var commercial = _commercialManager.GetAllProperties(pageNumber, pageSize);
            var residential = _residentialManager.GetAllResidentialProperty(pageNumber, pageSize);

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
