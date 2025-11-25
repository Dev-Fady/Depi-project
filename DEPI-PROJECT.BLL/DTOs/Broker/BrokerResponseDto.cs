using DEPI_PROJECT.BLL.DTOs.Property;
using DEPI_PROJECT.BLL.DTOs.User;

namespace DEPI_PROJECT.BLL.DTOs.Broker
{
    public class BrokerResponseDto
    {
        public required Guid Id { get; set; }
        public required string NationalID { get; set; }
        public required string LicenseID { get; set; }
        public required UserResponseDto User { get; set; }
    }
}