namespace DEPI_PROJECT.DAL.Models
{
    public class Agent
    {
        public Guid Id { get; set; }  // Primary key for Agent table
        public Guid UserId { get; set; }  // Foreign key to User table
        public string AgencyName { get; set; } = string.Empty;
        public int TaxIdentificationNumber { get; set; }
        public float Rating { get; set; }
        public int ExperienceYears { get; set; }

        // Navigation properties
        public User User { get; set; }  // Navigation to User
        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
