using DEPI_PROJECT.DAL.Models.Enums;

namespace DEPI_PROJECT.DAL.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public UserRole RoleType { get; set; }

        public string RoleName => RoleType.ToString();

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
