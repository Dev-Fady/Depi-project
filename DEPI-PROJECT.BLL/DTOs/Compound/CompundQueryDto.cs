using DEPI_PROJECT.BLL.DTOs.Query;

namespace DEPI_PROJECT.BLL.DTOs.Compound
{
    public class CompoundQueryDto : PagedQueryDto
    {
        // Filteration
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
    }
}