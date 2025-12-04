using DEPI_PROJECT.BLL.DTOs.Amenity;
using DEPI_PROJECT.BLL.DTOs.Compound;
using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.PropertyGallery;
using DEPI_PROJECT.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEPI_PROJECT.BLL.DTOs.CommercialProperty
{
    public class CommercialPropertyReadDto : PropertyResponseDto
    {
        public required string BusinessType { get; set; }
        public required int FloorNumber { get; set; }
        public required bool HasStorage { get; set; }
    }
}
