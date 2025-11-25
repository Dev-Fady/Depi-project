namespace DEPI_PROJECT.BLL.DTOs.Broker
{
    public class BrokerUpdateDto
    {
        public required Guid UserId { get; set; }
        public string? NationalID { get; set; }
        public string? LicenseID { get; set; } 
    }
}