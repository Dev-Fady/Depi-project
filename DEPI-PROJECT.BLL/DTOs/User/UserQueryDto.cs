using DEPI_PROJECT.BLL.DTOs.Query;

namespace DEPI_PROJECT.BLL.DTOs.Agent
{
    public enum OrderByUserOptions
    {
        DataJoind = 0
    }
    public class UserQueryDto : PagedQueryDto
    {
        public OrderByUserOptions? OrderByOption { get; set; }
        public bool IsDesc { get; set; }
        public string? SearchText { get; set; }
    }
}