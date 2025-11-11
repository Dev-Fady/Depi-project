using DEPI_PROJECT.BLL.DTOs.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Dtos.Wishists
{
    public enum OrderByWishListOptions
    {
        CreatedAt = 0,
    }
    public class WishListQueryDto : PagedQueryDto
    {
        public bool IsDesc { get; set; }
        public OrderByWishListOptions OrderBy { get; set; }
    }
}
