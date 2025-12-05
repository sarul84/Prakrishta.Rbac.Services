using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class MaskingRule : BaseEntity
    {
        public Guid MaskingRuleId { get; set; }
        public Guid PiiFieldId { get; set; }
        public PiiField PiiField { get; set; } = default!;
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; } = default!;
        public string? MaskingType { get; set; }
        public string? MaskingPattern { get; set; }
        public string? Description { get; set; }

        public ICollection<RoleMaskingRule> RoleMaskingRules { get; set; } = new List<RoleMaskingRule>();
    }

}
