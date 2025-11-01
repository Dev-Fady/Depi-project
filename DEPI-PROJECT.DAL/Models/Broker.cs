namespace DEPI_PROJECT.DAL.Models
{
    public class Broker
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string NationalID { get; set; }
        public string LicenseID { get; set; }

        public User User { get; set; }
    }
}
