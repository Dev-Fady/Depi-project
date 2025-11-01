using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.User;

namespace DEPI_PROJECT.BLL.DTOs.Broker
{
    public class BrokerResponseDto
    {
        public Guid Id { get; set; }
        public string NationalID { get; set; }
        public string LicenseID { get; set; }
        public UserResponseDto User { get; set; }
    }
}