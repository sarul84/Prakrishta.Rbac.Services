namespace RbacService.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        IOrganizationRepository Organizations { get; }
        IDepartmentRepository Departments { get; }
        IRolePermissionRepository RolePermissions { get; }
        IUserRoleRepository UserRoles { get; }
        IOrgAccessMappingRepository OrgAccessMappings { get; }
        IEnumerationRepository Enumerations { get; }
        IPiiFieldRepository PiiFields { get; }
        IMaskingRuleRepository MaskingRules { get; }
        IRoleMaskingRuleRepository RoleMaskingRules { get; }
        IPiiAccessLogRepository PiiAccessLogs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
