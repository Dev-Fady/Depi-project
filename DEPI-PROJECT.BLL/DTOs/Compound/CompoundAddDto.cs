namespace DEPI_PROJECT.BLL.DTOs.Compound
{
    public class CompoundAddDto
    {
        public required string Name { get; set; }
        public required string City { get; set; }
        public required string Address { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
