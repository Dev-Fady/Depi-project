using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;
using DEPI_PROJECT.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.DTOs.CommercialProperty
{
    public class CommercialPropertyAddDto
    {
        public string City { get; set; }
        public string Address { get; set; }
        public string GoogleMapsUrl { get; set; }

        public PropertyType PropertyType { get; set; }
        public PropertyPurpose PropertyPurpose { get; set; }
        public PropertyStatus PropertyStatus { get; set; }

        public decimal Price { get; set; }
        public float Square { get; set; }
        public string Description { get; set; }

        public Guid AgentId { get; set; }
        public Guid? CompoundId { get; set; }

        public string BusinessType { get; set; }
        public int FloorNumber { get; set; }
        public bool HasStorage { get; set; }
        public AmenityAddDto Amenity { get; set; }
    }
}
