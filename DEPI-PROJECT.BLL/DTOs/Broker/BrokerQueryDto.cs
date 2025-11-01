using DEPI_PROJECT.BLL.DTOs.Query;

namespace DEPI_PROJECT.BLL.DTOs.Broker
{
    public enum OrderByBrokerOptions
    {
        NationalID = 0,
        LicenseID = 1
    }
    public class BrokerQueryDto : PagedQueryDto
    {
        public OrderByBrokerOptions? OrderByOption { get; set; }
        public bool IsDesc { get; set; }
        public string? NationalID { get; set; }
        public string? LicenseID { get; set; }
    }
}