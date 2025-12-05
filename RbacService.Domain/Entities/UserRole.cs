using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public Guid UserRoleId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
        public Guid? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }

}
