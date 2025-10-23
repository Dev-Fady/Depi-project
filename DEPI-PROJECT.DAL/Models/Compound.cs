namespace DEPI_PROJECT.DAL.Models
{
    public class Compound
    {
        public Guid CompoundId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
