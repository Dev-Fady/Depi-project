using AqarakDB.Models.Enums;
using System;

namespace AqarakDB.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public RolesEnum RoleType { get; set; }

        public string Name => RoleType.ToString();
    }
}
