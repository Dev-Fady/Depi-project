using DEPI_PROJECT.BLL.DTOs.Authentication;

namespace DEPI_PROJECT.BLL.DTOs.Broker
{
    public class BrokerCreateDto
    {
        public required Guid UserId { get; set; }
        public required string NationalID { get; set; }
        public required string LicenseID { get; set; }
    }
}