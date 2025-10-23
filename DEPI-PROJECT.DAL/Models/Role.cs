using DEPI_PROJECT.DAL.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace DEPI_PROJECT.DAL.Models
{
    public class Role : IdentityRole<Guid>
    {
        public override string? Name { get => base.Name; set => Enum.GetName(typeof(UserRole), value); }
    }
}
