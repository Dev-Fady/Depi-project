namespace DEPI_PROJECT.BLL.DTOs.Compound
{
    public class CompoundUpdateDto
    {
        public Guid? CompoundId { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
    }
}
