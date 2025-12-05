using MediatR;

namespace RbacService.Domain.Events
{
    public class PiiAccessedEvent : INotification
    {
        public Guid AccessLogId { get; }
        public Guid UserId { get; }
        public Guid PiiFieldId { get; }
        public Guid TargetUserId { get; }
        public DateTime AccessedAt { get; }
        public string? AccessReason { get; }

        public PiiAccessedEvent(Guid accessLogId, Guid userId, Guid piiFieldId, Guid targetUserId, DateTime accessedAt, string? accessReason)
        {
            AccessLogId = accessLogId;
            UserId = userId;
            PiiFieldId = piiFieldId;
            TargetUserId = targetUserId;
            AccessedAt = accessedAt;
            AccessReason = accessReason;
        }
    }
}
