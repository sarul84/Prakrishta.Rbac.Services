using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public Guid UserId { get; }
        public string Resource { get; }
        public string Action { get; }

        public AccessDeniedException(Guid userId, string resource, string action)
            : base($"Access denied for User {userId} on Resource '{resource}' with Action '{action}'.")
        {
            UserId = userId;
            Resource = resource;
            Action = action;
        }
    }
}
