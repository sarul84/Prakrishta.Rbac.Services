using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Exceptions
{
    public class PiiAccessViolationException : Exception
    {
        public Guid UserId { get; }
        public Guid PiiFieldId { get; }
        public string Reason { get; }

        public PiiAccessViolationException(Guid userId, Guid piiFieldId, string reason)
            : base($"PII access violation by User {userId} on PII Field {piiFieldId}. Reason: {reason}")
        {
            UserId = userId;
            PiiFieldId = piiFieldId;
            Reason = reason;
        }
    }
}
