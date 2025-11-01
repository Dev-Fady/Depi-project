using DEPI_PROJECT.BLL.DTOs.Authentication;

namespace DEPI_PROJECT.BLL.DTOs.Agent
{
    public class AgentCreateDto
    {
        public Guid UserId { get; set; }
        public string AgencyName { get; set; }
        public int TaxIdentificationNumber { get; set; }
        public int experienceYears { get; set; } 
    }
}