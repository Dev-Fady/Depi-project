namespace DEPI_PROJECT.BLL.DTOs.Query
{
    public class OrderQueryDto : PagedQueryDto
    {
        public string? OrderBy { get; set; }
        public bool OrderAscending { get; set; }
    }
}