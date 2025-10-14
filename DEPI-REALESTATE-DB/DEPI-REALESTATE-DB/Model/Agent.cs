namespace DEPI_REALESTATE_DB.Model
{
    public class Agent : User
    {
        public string AgencyName { get; set; }
        public int TaxIdentificationNumber { get; set; }
        public float Rating { get; set; }
        public int ExperienceYears { get; set; }
    }
}
