
namespace DEPI_PROJECT.BLL.DTOs.Property
{
    public class PropertyReadDto
    {
        public int TotalCommercial {get; set;}
        public int TotalResidential {get; set;}
        public int TotalAll {get; set;}
        public int TotalPage {get; set;}
        public bool IsNextPage {get; set;}
        public List<Object> Properties {get; set;}
    }
}