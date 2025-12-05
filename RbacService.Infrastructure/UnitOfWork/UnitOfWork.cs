using RbacService.Domain.Interfaces.Repositories;
using RbacService.Infrastructure.Data;

namespace RbacService.Infrastructure.UnitOfWork
{
    public class UnitOfWork(RbacDbContext context,
        IUserRepository users,
        IRoleRepository roles,
        IPermissionRepository permissions,
        IOrganizationRepository organizations,
        IDepartmentRepository departments,
        IRolePermissionRepository rolePermissions,
        IUserRoleRepository userRoles,
        IOrgAccessMappingRepository orgAccessMappings,
        IEnumerationRepository enumerations,
        IPiiFieldRepository piiFields,
        IMaskingRuleRepository maskingRules,
        IRoleMaskingRuleRepository roleMaskingRules,
        IPiiAccessLogRepository piiAccessLogs) : IUnitOfWork
    {
        private readonly RbacDbContext _context = context;

        public IUserRepository Users { get; } = users;
        public IRoleRepository Roles { get; } = roles;
        public IPermissionRepository Permissions { get; } = permissions;
        public IOrganizationRepository Organizations { get; } = organizations;
        public IDepartmentRepository Departments { get; } = departments;
        public IRolePermissionRepository RolePermissions { get; } = rolePermissions;
        public IUserRoleRepository UserRoles { get; } = userRoles;
        public IOrgAccessMappingRepository OrgAccessMappings { get; } = orgAccessMappings;
        public IEnumerationRepository Enumerations { get; } = enumerations;
        public IPiiFieldRepository PiiFields { get; } = piiFields;
        public IMaskingRuleRepository MaskingRules { get; } = maskingRules;
        public IRoleMaskingRuleRepository RoleMaskingRules { get; } = roleMaskingRules;
        public IPiiAccessLogRepository PiiAccessLogs { get; } = piiAccessLogs;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);

        public void Dispose() => _context.Dispose();
    }
}
