using MediatR;

namespace RbacService.Domain.Events
{
    public class PiiMaskedEvent : INotification
    {
        public Guid MaskingRuleId { get; }
        public Guid PiiFieldId { get; }
        public Guid RoleId { get; }
        public DateTime AppliedAt { get; }

        public PiiMaskedEvent(Guid maskingRuleId, Guid piiFieldId, Guid roleId, DateTime appliedAt)
        {
            MaskingRuleId = maskingRuleId;
            PiiFieldId = piiFieldId;
            RoleId = roleId;
            AppliedAt = appliedAt;
        }
    }
}
