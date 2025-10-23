namespace DEPI_PROJECT.DAL.Models
{
    public class Agent : User
    {
        public string AgencyName { get; set; }
        public int TaxIdentificationNumber { get; set; }
        public float Rating { get; set; }
        public int ExperienceYears { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
