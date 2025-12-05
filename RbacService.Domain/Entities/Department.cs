using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class Department : BaseEntity
    {
        public Guid DepartmentId { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? DataPolicy { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }

}
