namespace DEPI_PROJECT.BLL.DTOs.Agent
{
    public class AgentUpdateDto
    {
        public Guid AgentId { get; set; }
        public string? AgencyName { get; set; }
        public int? TaxIdentificationNumber { get; set; }
        public double? Rating { get; set; }
        public int? experienceYears { get; set; } 
    }
}