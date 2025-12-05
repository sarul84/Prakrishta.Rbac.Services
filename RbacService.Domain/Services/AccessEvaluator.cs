using RbacService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RbacService.Domain.Services
{
    public interface IAccessEvaluator
    {
        bool HasAccess(User user, Permission permission, Organization? targetOrg = null);
    }

    public class AccessEvaluator : IAccessEvaluator
    {
        public bool HasAccess(User user, Permission permission, Organization? targetOrg = null)
        {
            return user.UserRoles.Any(ur => ur.Role.RolePermissions.Any(rp => rp.PermissionId == permission.PermissionId));
        }
    }
}
