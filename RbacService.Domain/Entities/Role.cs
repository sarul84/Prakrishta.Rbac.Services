using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? ScopeType { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
