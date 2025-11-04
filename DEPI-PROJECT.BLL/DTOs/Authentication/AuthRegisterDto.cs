using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.BLL.DTOs.Authentication
{
    public class AuthRegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UserRoleOptions RoleDiscriminator { get; set; }

        // Agent Data if specified
        public string? AgencyName { get; set; }
        public int? TaxIdentificationNumber { get; set; }
        public int? experienceYears { get; set; } 

        // Broker Data if specfified
        public string? NationalID { get; set; }
        public string? LicenseID { get; set; }
    }
}