using RbacService.Domain.Entities;
using RbacService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Infrastructure.Mapper
{
    public static class EnumerationMapper
    {
        // Map DB Enumeration row to Domain Enum
        public static PermissionAction ToPermissionAction(Enumeration enumeration)
        {
            if (enumeration.Type != "PermissionAction")
                throw new ArgumentException("Invalid enumeration type");

            return Enum.Parse<PermissionAction>(enumeration.Code);
        }

        public static RoleScope ToRoleScope(Enumeration enumeration)
        {
            if (enumeration.Type != "RoleScope")
                throw new ArgumentException("Invalid enumeration type");

            return enumeration.Code switch
            {
                "Global" => RoleScope.Global,
                "Org" => RoleScope.Organization,
                _ => throw new ArgumentException($"Unknown RoleScope code: {enumeration.Code}")
            };
        }

        public static MaskingType ToMaskingType(Enumeration enumeration)
        {
            if (enumeration.Type != "MaskingType")
                throw new ArgumentException("Invalid enumeration type");

            return Enum.Parse<MaskingType>(enumeration.Code);
        }

        // Map Domain Enum back to DB Enumeration Code
        public static string ToCode(PermissionAction action) => action.ToString();
        public static string ToCode(RoleScope scope) => scope == RoleScope.Global ? "Global" : "Org";
        public static string ToCode(MaskingType maskingType) => maskingType.ToString();
    }
}
