using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateBroker.DAL.Models
{
    public class Broker : User
    {

		public int NationalID { get; set; }

        public int LicenseID { get; set; }

   }
}
