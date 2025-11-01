using DEPI_PROJECT.BLL.DTOs.Authentication;

namespace DEPI_PROJECT.BLL.DTOs.Broker
{
    public class BrokerCreateDto
    {
        public Guid UserId { get; set; }
        public string NationalID { get; set; }
        public string LicenseID { get; set; }
    }
}