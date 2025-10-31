
using DEPI_PROJECT.BLL.DTOs.CommercialProperty;
using DEPI_PROJECT.BLL.DTOs.ResidentialProperty;

namespace DEPI_PROJECT.BLL.DTOs.Property
{
    public class PropertyReadDto
    {
        public IEnumerable<CommercialPropertyReadDto> CommercialProperties { get; set; }
        public IEnumerable<ResidentialPropertyReadDto> ResidentialProperties { get; set; }
        
    }
}