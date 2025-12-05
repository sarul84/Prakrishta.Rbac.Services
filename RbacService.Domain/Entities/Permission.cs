using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public Guid PermissionId { get; set; }
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; } = default!;
        public string Resource { get; set; } = default!;
        public string Action { get; set; } = default!;
        public string? Scope { get; set; }
        public string? Condition { get; set; }
        public string? Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

}
