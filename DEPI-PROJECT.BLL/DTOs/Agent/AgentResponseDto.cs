using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.User;

namespace DEPI_PROJECT.BLL.DTOs.Agent
{
    public class AgentResponseDto
    {
        public Guid Id { get; set; }
        public string AgencyName { get; set; }
        public int TaxIdentificationNumber { get; set; }
        public double Rating { get; set; }
        public int experienceYears { get; set; }
        public UserResponseDto User { get; set; }
        public ICollection<PropertyResponseDto> Properties { get; set; }
    }
}