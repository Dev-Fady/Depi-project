using DEPI_PROJECT.BLL.DTOs.Query;
using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.Property
{
    public enum OrderByOptions
    {
        Price = 1,
        Sqaure = 2,
        DateListed = 3
    }

    public class PropertyQueryDto : PagedQueryDto
    {
        public OrderByOptions? OrderBy { get; set; }

        public bool IsDesc { get; set; }

        // Filtering
        public string? Title { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public PropertyType? PropertyType { get; set; }
        public PropertyPurpose? PropertyPurpose { get; set; }
        public PropertyStatus? PropertyStatus { get; set; }
        public decimal? UpToPrice { get; set; }
        public double? UpToSquare { get; set; } 
    }
}