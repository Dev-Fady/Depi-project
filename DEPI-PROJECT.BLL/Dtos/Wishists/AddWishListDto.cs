using DEPI_PROJECT.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.Dtos.Wishists
{
    public class AddWishListDto
    {
        public Guid UserID { get; set; }
        public Guid PropertyID { get; set; }
    }
}
