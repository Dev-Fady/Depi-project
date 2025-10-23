using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RealEstateBroker.DAL.Enums.Kitchen;

namespace RealEstateBroker.DAL.Models
{
    public class ResidentialProperty : Property
    {
        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public int Floors { get; set; }

        public KitchenType KitchenType { get; set; }
    }
}
