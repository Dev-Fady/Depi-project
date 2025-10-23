using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RealEstateBroker.DAL.Models
{
    public class CommercialProperty : Property
    {
        public string BusinessType { get; set; }
        public int FloorNumber { get; set; }
        public bool HasStorage { get; set; }
    }
}
