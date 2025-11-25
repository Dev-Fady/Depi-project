
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;

namespace DEPI_PROJECT.BLL.DTOs.Property
{
    public class AllPropertyReadDto
    {
        public required IEnumerable<CommercialPropertyReadDto> CommercialProperties { get; set; }
        public required IEnumerable<ResidentialPropertyReadDto> ResidentialProperties { get; set; }
        
    }
}