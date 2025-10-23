using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RealEstateBroker.DAL.Models
{
    public class Amenity
    {
        [Key]
        public Guid PropertyID { get; set; }
        public bool HasElectricityLine { get; set; }
        public bool HasWaterLine { get; set; }

        public bool HasGasLine { get; set; }

        public virtual Property Property { get; set; }
    }
}
