using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.User;

namespace DEPI_PROJECT.BLL.DTOs.Agent
{
    public class AgentResponseDto
    {
        public required Guid Id { get; set; }
        public required string AgencyName { get; set; }
        public int TaxIdentificationNumber { get; set; }
        public double Rating { get; set; }
        public int experienceYears { get; set; }
        public required UserResponseDto User { get; set; }
        public required ICollection<PropertyResponseDto> Properties { get; set; }
    }
}