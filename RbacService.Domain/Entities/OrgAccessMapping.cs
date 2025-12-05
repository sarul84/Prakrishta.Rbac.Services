using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class OrgAccessMapping : BaseEntity
    {
        public Guid AccessMappingId { get; set; }
        public Guid SourceOrganizationId { get; set; }
        public Organization SourceOrganization { get; set; } = default!;
        public Guid TargetOrganizationId { get; set; }
        public Organization TargetOrganization { get; set; } = default!;
    }

}
