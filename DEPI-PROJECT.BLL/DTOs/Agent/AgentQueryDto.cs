using DEPI_PROJECT.BLL.DTOs.Query;

namespace DEPI_PROJECT.BLL.DTOs.Agent
{
    public enum OrderByAgentOptions
    {
        Rating = 0,
        experienceYears = 1
    }
    public class AgentQueryDto : PagedQueryDto
    {
        public OrderByAgentOptions? OrderByOption { get; set; }
        public bool IsDesc { get; set; }
        public string? AgencyName { get; set; }
        public double? MinRating { get; set; }
        public int? MinexperienceYears { get; set; } 
    }
}