using DEPI_REALESTATE_DB.Model.Enums;

namespace DEPI_REALESTATE_DB.Model
{
    public class Role
    {
        public Guid Id { get; set; }
        public UserRole RoleType { get; set; }

        public string RoleName => RoleType.ToString();

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
