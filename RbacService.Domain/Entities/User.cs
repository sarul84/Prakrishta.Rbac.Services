using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; } = default!;
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? Designation { get; set; }
        public Guid? ManagerId { get; set; }
        public User? Manager { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public Guid ApplicationId { get; set; }

        public Application? Application { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
