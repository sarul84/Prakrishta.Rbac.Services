using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Entities
{
    public class PiiAccessLog : BaseEntity
    {
        public Guid AccessLogId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public Guid PiiFieldId { get; set; }
        public PiiField PiiField { get; set; } = default!;
        public Guid TargetUserId { get; set; }
        public User TargetUser { get; set; } = default!;
        public DateTime AccessedAt { get; set; }
        public string? AccessReason { get; set; }
        public Guid? ApplicationId { get; set; }
        public Application? Application { get; set; }
        public Guid? RoleId { get; set; }
        public Role? Role { get; set; }
        public bool IsMasked { get; set; }
        public string? AccessChannel { get; set; }
        public string? AccessOutcome { get; set; }
        public string? IPAddress { get; set; }
        public string? CorrelationId { get; set; }

    }
}
