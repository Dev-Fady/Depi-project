using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Dtos
{
    public class GetAllWishListDto
    {
        public Guid UserID { get; set; }
        public Guid PropertyID { get; set; }
        public Guid ListingID { get; set; }
        public decimal Price {  get; set; }
        public string Title { get; set; }

    }
}
