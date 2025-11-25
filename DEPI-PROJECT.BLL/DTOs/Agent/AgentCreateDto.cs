using DEPI_PROJECT.BLL.DTOs.Authentication;

namespace DEPI_PROJECT.BLL.DTOs.Agent
{
    public class AgentCreateDto
    {
        public required Guid UserId { get; set; }
        public required string AgencyName { get; set; }
        public int TaxIdentificationNumber { get; set; }
        public int experienceYears { get; set; } 
    }
}