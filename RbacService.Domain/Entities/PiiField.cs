using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class PiiField : BaseEntity
    {
        public Guid PiiFieldId { get; set; }
        public string Code { get; set; } = default!;
        public string? Label { get; set; }
        public string? Description { get; set; }

        public ICollection<MaskingRule> MaskingRules { get; set; } = new List<MaskingRule>();
    }

}
