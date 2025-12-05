using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Enums
{
    public enum MaskingType
    {
        Full = 1,
        Partial = 2,
        Initials = 3,
        Hash = 4,
        Redact = 5,
        Custom = 6
    }
}
