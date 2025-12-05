using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class Application : BaseEntity
    {
        public Guid ApplicationId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }

}
