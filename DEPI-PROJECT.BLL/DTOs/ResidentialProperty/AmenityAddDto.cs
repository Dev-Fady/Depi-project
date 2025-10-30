using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.DTOs.ResidentialProperty
{
    public class AmenityAddDto
    {
        public bool HasElectricityLine { get; set; }
        public bool HasWaterLine { get; set; }
        public bool HasGasLine { get; set; }
    }
}
