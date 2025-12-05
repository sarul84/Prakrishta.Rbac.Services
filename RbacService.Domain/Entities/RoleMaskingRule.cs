using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class RoleMaskingRule : BaseEntity
    {
        public Guid RoleMaskingRuleId { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; } = default!;
        public Guid MaskingRuleId { get; set; }
        public MaskingRule MaskingRule { get; set; } = default!;
    }

}
