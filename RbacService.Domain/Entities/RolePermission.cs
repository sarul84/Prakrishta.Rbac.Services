using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class RolePermission : BaseEntity
    {
        public Guid RolePermissionId { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; } = default!;
    }

}
